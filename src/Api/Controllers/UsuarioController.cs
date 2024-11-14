using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Roles.Queries.GetRoles;
using Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserById;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserName;
using Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers;
using Ecommerce.Application.Features.Auths.Users.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;


    public UsuarioController(IManageImageService manageImageService, IMediator mediator)
    {
        _manageImageService = manageImageService;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
    {
        if (request.Foto is not null)
        {
            var resultImage = await _manageImageService.UploadImage(new ImageData
            {
                ImageStream = request.Foto!.OpenReadStream(),
                Nombre = request.Foto.Name
            });

            request.FotoId = resultImage.PublicId;
            request.FotoUrl = resultImage.Url;

        }

        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("forgotpassword", Name = "ForgotPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("resetpassword", Name = "ResetPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("updatepassword", Name = "UpdatePassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut("update", Name = "Update")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
    {
        if (request.Foto is not null)
        {
            var resultImage = await _manageImageService.UploadImage(new ImageData
            {
                ImageStream = request.Foto!.OpenReadStream(),
                Nombre = request.Foto!.Name
            }
            );

            request.FotoId = resultImage.PublicId;
            request.FotoUrl = resultImage.Url;
        }


        return await _mediator.Send(request);
    }
    [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
    [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Usuario>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
    [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Usuario>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
    [HttpGet("{id}", Name = "GetUsuarioById")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> GetUsuarioiById(string id)
    {
        var query = new GetUserByIdQuery(id);
        return await _mediator.Send(query);
    }

    [HttpGet("", Name = "CurretUserSession")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> CurrentUserSession()
    {
        var query = new GetUserByTokenQuery();
        return await _mediator.Send(query);
    }

    [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
    [HttpGet("username/{username}", Name = "GetUsuarioByUserName")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> GetUsuarioByUserName(string username)
    {
        var query = new GetUserByUserNameQuery(username);
        return await _mediator.Send(query);
    }

    [Authorize(Roles = Application.Models.Authorization.Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationUserAdmin")]
    [ProducesResponseType(typeof(PaginationVM<Usuario>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVM<Usuario>>> PaginationUserAdmin(
        [FromQuery] PaginationUsersQuery paginationUserQuery
    )
    {
        var paginationUser = await _mediator.Send(paginationUserQuery);
        return Ok(paginationUser);
    }

    [AllowAnonymous]
    [HttpGet("roles", Name ="GetRolesList")]
    [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<string>>> GetRolesList()
    {
        var query = new GetRolesQuery();
        return Ok(await _mediator.Send(query));
    }
}