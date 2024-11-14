using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommand : IRequest<Usuario>
    {
        public string? Id { get; set; }
    }
}
