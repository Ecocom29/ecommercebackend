using Ecommerce.Application.Features.Auths.Users.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<AuthResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}