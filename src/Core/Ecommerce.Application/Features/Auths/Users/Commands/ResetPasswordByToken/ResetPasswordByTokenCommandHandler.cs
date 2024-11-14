using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {
        private readonly UserManager<Usuario> _userManager;

        public ResetPasswordByTokenCommandHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Método para resetear el password mediante un Token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                throw new BadRequestException("El password no es igual a la confirmacion del password");
            }

            var updateUsuario = await _userManager.FindByEmailAsync(request.Email!);
            if (updateUsuario is null)
            {
                throw new BadRequestException("El email no esta registrado como usuario");
            }

            var token = Convert.FromBase64String(request.Token!);
            var tokenResult = Encoding.UTF8.GetString(token);

            var resetResultado = await _userManager.ResetPasswordAsync(updateUsuario, tokenResult, request.Password!);
            if (!resetResultado.Succeeded)
            {
                throw new Exception("No se pudo resetear el password");
            }


            return $"Se actualizo exitosamente tu password ${request.Email}";
        }
    }
}
