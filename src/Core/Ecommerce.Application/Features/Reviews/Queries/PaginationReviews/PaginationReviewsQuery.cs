using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Reviews.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Queries.PaginationReviews
{
    public class PaginationReviewsQuery : PaginationBaseQuery, IRequest<PaginationVM<ReviewVM>>
    {
        public int? ProductId { get; set; }
    }
}
