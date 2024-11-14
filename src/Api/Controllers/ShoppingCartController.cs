using Ecommerce.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem;
using Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart;
using Ecommerce.Application.Features.ShoppingCarts.Queries.GetShoppingCartById;
using Ecommerce.Application.Features.ShoppingCarts.VMS;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVM>> GetShoppingCart(Guid id)
        {
            //Validacion del GUID 
            var shoppingCartId = id == Guid.Empty ? Guid.NewGuid() : id;

            var query = new GetShoppingCartByIdQuery(shoppingCartId);

            return await _mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPut("{id}", Name = "UpdateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVM>> UpdateShoppingCart(Guid id, UpdateShoppingCartCommand request)
        {
            //Añadir el ID
            request.ShoppingCartId = id;
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpDelete("item/{id}", Name = "DeleteShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartVM>> DeleteShoppingCart(int id)
        {
            //Añadir el ID            
            return await _mediator.Send(new DeleteShoppingCartItemCommand() { Id = id });
        }
    }
}
