using Ecommerce.Application.Features.ShoppingCarts.VMS;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartVM>
    {
        public Guid? ShoppingCartId { get; set; }

        public GetShoppingCartByIdQuery(Guid? shoppingCartId)
        {
            ShoppingCartId = shoppingCartId ?? throw new ArgumentNullException(nameof(shoppingCartId));
        }
    }
}
