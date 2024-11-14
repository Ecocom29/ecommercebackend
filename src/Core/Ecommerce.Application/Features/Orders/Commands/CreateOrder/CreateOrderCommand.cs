using Ecommerce.Application.Features.Orders.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderVM>
    {
        public Guid? ShoppingCartId { get; set; }
    }
}
