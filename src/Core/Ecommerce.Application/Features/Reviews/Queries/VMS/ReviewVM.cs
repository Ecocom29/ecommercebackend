namespace Ecommerce.Application.Features.Reviews.VMS;

public class ReviewVM
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int Rating { get; set; }
    public string? Comentario { get; set; }
    public int ProductId { get; set; }
}