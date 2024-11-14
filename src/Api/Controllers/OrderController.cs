using Ecommerce.Application.Features.Adresses.Command.CreateAddress;
using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Features.Orders.Commands.CreateOrder;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Application.Features.Orders.Queries.GetOrdersById;
using Ecommerce.Application.Features.Orders.Queries.PaginationOrder;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IAuthService _authService;

        public OrderController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("address", Name = "CreateAddress")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AddressVM>> CreateAddress([FromBody] CreateAddressCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVM>> CreateOrder([FromBody] CreateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVM>> UpdateOrder([FromBody] UpdateOrderCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(typeof(OrderVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVM>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            return Ok(await _mediator.Send(query));
        }


        /// <summary>
        /// Paginacion para el usuario invitado
        /// </summary>
        /// <param name="paginationsParams"></param>
        /// <returns></returns>
        [HttpGet("paginationByUserName", Name = "PaginationByUserName")]
        [ProducesResponseType(typeof(OrderVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVM<OrderVM>>> PaginationOrderByUserName([FromQuery] PaginationOrdersQuery paginationsParams)
        {
            paginationsParams.UserName = _authService.GetSessionUser(); //Obtener el username

            var pagination = await _mediator.Send(paginationsParams);

            return Ok(pagination);

        }


        /// <summary>
        /// Pagination para el ADMIN
        /// </summary>
        /// <param name="paginationsParams"></param>
        /// <returns>Retornar la paginación para el admin</returns>
        [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
        [HttpGet("paginationOrderAdmin", Name = "PaginationOrder")]
        [ProducesResponseType(typeof(OrderVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVM<OrderVM>>> PaginationOrder([FromQuery] PaginationOrdersQuery paginationsParams)
        {
            paginationsParams.UserName = _authService.GetSessionUser(); //Obtener el username

            var pagination = await _mediator.Send(paginationsParams);

            return Ok(pagination);
        }
    }
}
