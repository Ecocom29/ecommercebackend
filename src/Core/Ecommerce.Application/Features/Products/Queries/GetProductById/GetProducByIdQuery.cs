using Ecommerce.Application.Features.Products.Queries.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetProductById;

public class GetProducByIdQuery : IRequest<ProductVM>
{
    public int ProductId { get; set; }

    public GetProducByIdQuery(int productId)
    {
        ProductId = productId == 0 ? throw new ArgumentNullException(nameof(productId)) : productId;
    }
}