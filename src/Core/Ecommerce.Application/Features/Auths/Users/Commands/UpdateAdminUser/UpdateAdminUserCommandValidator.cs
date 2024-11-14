using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Identity;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : IRequestHandler<UpdateAdminUserCommand, Usuario>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateAdminUserCommandValidator(
            UserManager<Usuario> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IAuthService authService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<Usuario> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var updateUsuario = await _userManager.FindByIdAsync(request.Id!);

            if (updateUsuario is null)
            {
                throw new BadRequestException("El usuario no existe.");
            }

            updateUsuario.Nombre = request.Nombre;
            updateUsuario.Apellido = request.Apellido;
            updateUsuario.Telefono = request.Telefono;

            var resultado = await _userManager.UpdateAsync(updateUsuario);

            if (!resultado.Succeeded)
            {
                throw new Exception("No se pudo actualizar el usuario.");
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);

            if(role is null)
            {
                throw new Exception("El Rol asignado no existe.");
            }

            await _userManager.AddToRoleAsync(updateUsuario, role.Name!);

            return updateUsuario;

        }
    }
}
