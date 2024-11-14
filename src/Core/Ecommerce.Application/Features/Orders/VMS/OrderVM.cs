using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Models.Order;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.Orders.VMS
{
    public class OrderVM
    {
        public int Id { get; set; }
        public AddressVM? OrderAddress { get; set; }
        public List<OrderItemVM>? OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioEnvio { get; set; }

        public OrderStatus Status { get; set; }


        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }
        public string? StripeApiKey { get; set; }
        public string? CompradorUserName { get; set; }

        public string? CompradorNombre { get; set; }

        public int Cantidad
        {
            get
            {
                return OrderItems!.Sum(x => x.Cantidad);
            }
            set { }
        }

        public string? StatusLabel
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.Completed:
                        {
                            return OrderStatusLabel.COMPLETED;
                        }

                    case OrderStatus.Pending:
                        {
                            return OrderStatusLabel.PENDING;
                        }
                    case OrderStatus.Enviado:
                        {
                            return OrderStatusLabel.ENVIADO;
                        }
                    case OrderStatus.Error:
                        {
                            return OrderStatusLabel.ERROR;
                        }
                    default:
                        {
                            return OrderStatusLabel.ERROR;
                        }
                }
            }
            set { }
        }
    }
}
