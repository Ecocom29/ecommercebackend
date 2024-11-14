namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductImageCommand
    {
        public string? Url { get; set; }
        public int ProductId    { get; set; }
        public string? PublicCode { get; set; }
    }
}
