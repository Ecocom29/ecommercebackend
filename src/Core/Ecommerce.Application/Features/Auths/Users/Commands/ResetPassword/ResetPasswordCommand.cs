using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
    }
}
