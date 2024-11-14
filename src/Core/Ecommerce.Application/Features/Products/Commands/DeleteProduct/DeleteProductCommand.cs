using Ecommerce.Application.Features.Products.Queries.VMS;
using MediatR;
using MimeKit.Encodings;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<ProductVM>
    {

        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException(nameof(ProductId)) : productId;
        }
    }
}
