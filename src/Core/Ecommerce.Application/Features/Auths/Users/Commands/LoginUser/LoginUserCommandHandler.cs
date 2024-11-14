using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Features.Auths.Users.VMS;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Stripe;

namespace Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authServices;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authServices,
            IMapper mapper,
            IUnitOfWork unitOfWork
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _authServices = authServices;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new NotFoundException(nameof(Usuario), request.Email!);
        }

        if (!user.IsActive)
        {
            throw new Exception($"El usuario esta bloqueado, contacte al admin");
        }

        var resultado = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

        if (!resultado.Succeeded)
        {
            throw new Exception("Las credenciales del usuario son erroneas");
        }

        var direccionEnvio = await _unitOfWork.Repository<Domain.Address>().GetEntityAsync(
            x => x.Username == user.UserName
        );

        var roles = await _userManager.GetRolesAsync(user);

        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
            Telefono = user.Telefono,
            Email = user.Email,
            UserName = user.UserName,
            Avatar = user.AvatarUrl,
            DireccionEnvio = _mapper.Map<AddressVM>(direccionEnvio),
            Token = _authServices.CreateToken(user, roles),
            Roles = roles
        };

        return authResponse;
    }
}