using Ecommerce.Application.Features.Image.Queries.VMS;
using Ecommerce.Application.Features.Reviews.VMS;
using Ecommerce.Application.Models.Product;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.Products.Queries.VMS;

public class ProductVM
{
    public int ID { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Rating { get; set; }
    public string? Vendedor { get; set; }
    public int Stock { get; set; }
    public string? Descripcion { get; set; }
    public virtual ICollection<ReviewVM>? Reviews { get; set; }
    public virtual ICollection<ImagenVM>? Images { get; set; }

    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int NumeroReviews { get; set; }
    public ProductStatus Status { get; set; }
    public string? StatusLabel
    {
        get
        {
            switch (Status)
            {
                case ProductStatus.Activo:
                {
                    return ProductStatusLabel.ACTIVO;
                }
                case ProductStatus.Inactivo:
                {
                    return ProductStatusLabel.INACTIVO;
                }
                default: return ProductStatusLabel.INACTIVO;
            }
        }
        set { }
    }
}