using Ecommerce.Application.Features.Products.Queries.VMS;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductVM>
    {
        public string? Nombre{ get; set; }

        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? Vendedor { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Foto {  get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImageURLs { get; set; }
    }
}
