using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderVM>
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
