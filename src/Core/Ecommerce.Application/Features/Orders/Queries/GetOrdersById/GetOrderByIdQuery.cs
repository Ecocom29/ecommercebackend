using Ecommerce.Application.Features.Orders.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrdersById
{
    public class GetOrderByIdQuery : IRequest<OrderVM>
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            this.OrderId = orderId == 0 ? throw new ArgumentNullException(nameof(orderId)) : orderId;
        }
    }
}
