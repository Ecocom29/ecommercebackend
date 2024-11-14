using Ecommerce.Application.Features.ShoppingCarts.VMS;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommand : IRequest<ShoppingCartVM>
    {
        public int Id { get; set; }
    }
}
