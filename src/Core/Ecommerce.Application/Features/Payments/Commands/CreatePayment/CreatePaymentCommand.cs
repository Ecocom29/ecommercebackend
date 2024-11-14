using Ecommerce.Application.Features.Orders.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<OrderVM>
    {
        public int OrderId  { get; set; }
        public Guid? ShoppingCartMasterId { get; set; }

    }
}
