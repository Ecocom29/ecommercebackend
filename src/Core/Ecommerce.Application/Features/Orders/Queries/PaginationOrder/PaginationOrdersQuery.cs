using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrder
{
    public class PaginationOrdersQuery: PaginationBaseQuery, IRequest<PaginationVM<OrderVM>>
    {
        public int? Id { get; set; }
        public string? UserName {  get; set; }
    }
}
