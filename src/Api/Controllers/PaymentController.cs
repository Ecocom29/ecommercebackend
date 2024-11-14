using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Features.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        public IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreatePayment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderVM>> CreatePayment([FromBody] CreatePaymentCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
