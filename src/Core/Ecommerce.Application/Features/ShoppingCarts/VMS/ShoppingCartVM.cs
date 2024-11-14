using Ecommerce.Domain;

namespace Ecommerce.Application.Features.ShoppingCarts.VMS
{
    public class ShoppingCartVM
    {
        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItemVM>? ShoppingCartItems { get; set; }

        public decimal Total
        {
            get
            {
                return

                        Math.Round(
                            ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad) +
                            (ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) * Convert.ToDecimal(0.18) +
                            ((ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25)
                        , 2
                        );

            }
            set { }
        }


        public int Cantidad
        {
            get { return ShoppingCartItems!.Sum(x => x.Cantidad); }
            set { }
        }

        public decimal SubTotal
        {
            get { return Math.Round(ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad), 2); }
        }

        public decimal Impuesto
        {
            get
            {
                return Math.Round(((ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) * Convert.ToDecimal(0.18)), 2);
            }
            set { }
        }

        public decimal PrecioEnvio
        {
            get
            {
                return (ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25;
            }

            set { }
        }
    }
}
