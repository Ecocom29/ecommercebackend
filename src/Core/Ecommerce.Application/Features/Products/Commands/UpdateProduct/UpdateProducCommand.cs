using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.VMS;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProducCommand : IRequest<ProductVM>
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? Vendedor { get; set; }
        public int Stock { get; set; }
        public string? CateogryId { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImagesUrls { get; set; }
    }
}
