using Ecommerce.Application.Features.Countries.Queries.GetCountryList;
using Ecommerce.Application.Features.Countries.VMS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController : ControllerBase
    {
        private IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(typeof(IReadOnlyList<CountryVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CountryVM>>> GetCountries()
        {
            var query = new GetCountryListQuery();
            return Ok(await _mediator.Send(query));
        }

    }
}
