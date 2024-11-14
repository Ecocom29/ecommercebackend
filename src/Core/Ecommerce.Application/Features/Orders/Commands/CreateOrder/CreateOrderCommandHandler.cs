using AutoMapper;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IAuthService _authService;
        private readonly UserManager<Usuario> _userManager;
        private readonly StripeSettings _stripeSettings;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthService authService,
            UserManager<Usuario> userManager,
            IOptions<StripeSettings> stripeSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<OrderVM> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //Eliminar la order pendiente del usuario (Buscar PENDIENTES)

            var orderPending = await _unitOfWork.Repository<Order>().GetEntityAsync(

                x => x.CompradorUsername == _authService.GetSessionUser() && x.Status == OrderStatus.Pending,
                null,
                true
            );


            if (orderPending is not null)
            {
                await _unitOfWork.Repository<Order>().DeleteAsync(orderPending);
            }

            var includes = new List<Expression<Func<ShoppingCart, object>>>();

            includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Producto));

            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes,
                false
             );


            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new Exception("El usuario no esta autenticado.");
            }

            var direccion = await _unitOfWork.Repository<Domain.Address>().GetEntityAsync(
                x => x.Username == user.UserName,
                null,
                false
            );

            OrderAddress orderAddress = new()
            {
                Direccion = direccion.Direccion,
                Ciudad = direccion.Ciudad,
                CodigoPostal = direccion.CodigoPostal,
                Pais = direccion.Pais,
                Departamento = direccion.Departamento,
                Username = direccion.Username
            };

            await _unitOfWork.Repository<OrderAddress>().AddAsync(orderAddress); //Insertar la direccion de orden de compra

            //Obtener totales
            var subtotal = Math.Round(shoppingCart.ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad));

            var impuesto = Math.Round(subtotal * Convert.ToDecimal(0.18), 2);

            var precioEnvio = subtotal < 100 ? 10 : 25;

            var total = subtotal + impuesto + precioEnvio;

            var nombreComprador = $"{user.Nombre} {user.Apellido}";

            var order = new Order(
                nombreComprador,
                user.UserName!,
                orderAddress,
                subtotal,
                total,
                impuesto,
                precioEnvio
            );

            await _unitOfWork.Repository<Order>().AddAsync(order);

            #region Obtener los elementos para el detalle de compra
            var items = new List<OrderItem>();

            foreach (var shoppingElement in shoppingCart.ShoppingCartItems!)
            {
                var orderItem = new OrderItem
                {
                    ProductNombre = shoppingElement.Producto,
                    ProductId = shoppingElement.ProductId,
                    ImagenUrl = shoppingElement.Imagen,
                    Precio = shoppingElement.Precio,
                    Cantidad = shoppingElement.Cantidad,
                    OrderId = shoppingElement.Id
                };

                items.Add(orderItem);
            }

            _unitOfWork.Repository<OrderItem>().AddRange(items);

            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("Error al crear la orden de compra.");
            }

            #endregion

            #region Configuración de Stripe pay
            
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var servicePay = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)order.Total,
                    Currency = "MXN",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await servicePay.CreateAsync(options);
                order.PaymentIntentId = intent.Id;
                order.ClientSecret = intent.ClientSecret;
                order.StripeApiKey = _stripeSettings.PublishbleKey;

            }
            else
            {
                //Cuando exista una orden de compra
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)order.Total
                };

                await servicePay.UpdateAsync(order.PaymentIntentId, options);
            }

            #endregion


            _unitOfWork.Repository<Order>().UpdateEntity(order);
            var resultOrder = await _unitOfWork.Complete();

            if (resultOrder <= 0)
            {
                throw new Exception("Error al crear el Payment intent en Stripe.");
            }

            return _mapper.Map<OrderVM>(order);
        }
    }
}
