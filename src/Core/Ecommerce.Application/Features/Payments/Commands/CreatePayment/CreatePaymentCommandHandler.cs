using AutoMapper;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OrderVM>
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CreatePaymentCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVM> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            #region Actualización de la orden de compra
            var orderToPay = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x=> x.Id == request.OrderId,
                null,
                false
                );

            orderToPay.Status = OrderStatus.Completed;

            _unitOfWork.Repository<Order>().UpdateEntity(orderToPay);
            #endregion

            var shoppingCartItemsItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                x=>x.ShoppingCartMasterId == request.ShoppingCartMasterId
                );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItemsItems);

            await _unitOfWork.Complete();

            return _mapper.Map<OrderVM>(orderToPay);

        }
    }
}
