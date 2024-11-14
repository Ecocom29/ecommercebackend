using Ecommerce.Application.Features.ShoppingCarts.VMS;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommand :IRequest<ShoppingCartVM>
    {
        public Guid? ShoppingCartId { get; set; }

        public List<ShoppingCartItemVM>? ShoppingCartItems { get; set; }
    }
}
