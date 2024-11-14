using Ecommerce.Application.Features.Auths.Users.VMS;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
    {
        public readonly UserManager<Usuario> _userManager;

        public GetUserByIdQueryHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId!);


            if (user is null)
            {
                throw new Exception("El Usuario no existe.");
            }

            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                UserName = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
