using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.ShoppingCarts.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Stripe;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVM> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartToUpdate = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                p => p.ShoppingCartMasterId == request.ShoppingCartId
            );

            if (shoppingCartToUpdate is null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId!);
            }

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId
            );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);


            var shoppingCartItemsToAdd = _mapper.Map<List<ShoppingCartItem>>(request.ShoppingCartItems);

            shoppingCartItemsToAdd.ForEach(x => {
                x.ShoppingCartId = shoppingCartToUpdate.Id;
                x.ShoppingCartMasterId = request.ShoppingCartId;
            });

            _unitOfWork.Repository<ShoppingCartItem>().AddRange(shoppingCartItemsToAdd);

            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("No se pudo agregar productos items al carrito de compras");
            }



            var includes = new List<Expression<Func<ShoppingCart, object>>>();
            includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Producto));
            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes,
                true
            );


            return _mapper.Map<ShoppingCartVM>(shoppingCart);
        }
    }
}
