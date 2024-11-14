using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public ProductController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVM>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVM>>> GetProductList()
    {
        var query = new GetProductListQuery();
        var productos = await _mediator.Send(query);
        return Ok(productos);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationProduct")]
    [ProducesResponseType(typeof(PaginationVM<ProductVM>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVM<ProductVM>>> PaginationProduct(
            [FromQuery] PaginationProductsQuery paginationProductParams
        )
    {
        paginationProductParams.Status = ProductStatus.Activo;
        var paginationProduct = await _mediator.Send(paginationProductParams);

        return Ok(paginationProduct);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationProductAdmin")]
    [ProducesResponseType(typeof(PaginationVM<ProductVM>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVM<ProductVM>>> PaginationProductAdmin(
            [FromQuery] PaginationProductsQuery paginationProductParams
        )
    {        
        var paginationProduct = await _mediator.Send(paginationProductParams);
        return Ok(paginationProduct);
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVM>> GetProductById(int Id)
    {
        var query = new GetProducByIdQuery(Id);
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Subir imagenes del producto
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("create", Name = "CreateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVM>> CreateProduct([FromForm] CreateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if(request.Foto is not null)
        {
            foreach (var item in request.Foto)
            {
                var resultImages = await _manageImageService.UploadImage(
                    new ImageData
                    {
                        ImageStream = item.OpenReadStream(),
                        Nombre = item.Name
                    });

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImages.PublicId,
                    Url = resultImages.Url,
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImageURLs = listFotoUrls;
        }

        return await _mediator.Send(request);

    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("update", Name = "UpdateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVM>> UpdateProduct([FromForm] UpdateProducCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if (request.Fotos is not null)
        {
            foreach (var item in request.Fotos)
            {
                var resultImages = await _manageImageService.UploadImage(
                    new ImageData
                    {
                        ImageStream = item.OpenReadStream(),
                        Nombre = item.Name
                    }
                );

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImages.PublicId,
                    Url = resultImages.Url,
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImagesUrls = listFotoUrls;
        }

        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("status/{id}", Name = "UpdateStatusProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVM>> UpdateStatusProduct(int id)
    {
        var request = new DeleteProductCommand(id);

        return await _mediator.Send(request);
    }
}