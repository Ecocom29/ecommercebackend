using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts;


public class PaginationProductsQuery : PaginationBaseQuery, IRequest<PaginationVM<ProductVM>>
{
    public int? CategoryId { get; set; }
    public decimal? PrecioMax { get; set; }
    public decimal? PrecioMin { get; set; }
    public int? Rating { get; set; }
    public ProductStatus? Status { get; set; }
}