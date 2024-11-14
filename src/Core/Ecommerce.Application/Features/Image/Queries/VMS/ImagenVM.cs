namespace Ecommerce.Application.Features.Image.Queries.VMS;

public class ImagenVM
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public int ProductId { get; set; }
    public string? PublicCode { get; set; }
}