using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Identity;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordHandler(IAuthService authService, UserManager<Usuario> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var updateUsusario = await _userManager.FindByEmailAsync(_authService.GetSessionUser());

            if (updateUsusario is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            var resultValidateOldPassword = _userManager.PasswordHasher
                .VerifyHashedPassword(updateUsusario, updateUsusario.PasswordHash!, request.OldPassword!);

            if(!(resultValidateOldPassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestException("El actual password ingresado es incorrecto");
            }

            var hashNewPassword = _userManager.PasswordHasher.HashPassword(updateUsusario, request.NewPassword!);
            updateUsusario.PasswordHash = hashNewPassword;

            var result = await _userManager.UpdateAsync(updateUsusario);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo resetear el Password");
            }

            return Unit.Value;
        }
    }
}
