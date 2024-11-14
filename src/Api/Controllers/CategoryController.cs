using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using Ecommerce.Application.Features.Categories.VMS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("List", Name = "GetCategoryList")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryVM>>> GetCategoryList()
        {
            var query = new GetCategoryListQuery();
            return Ok(await _mediator.Send(query));
        }

    }
}
