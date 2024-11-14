namespace Ecommerce.Application.Features.Orders.VMS
{
    public class OrderItemVM
    {
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int OrderId { get; set; }
        public int ProducItemId { get; set; }
        public string? ProductNombre { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
