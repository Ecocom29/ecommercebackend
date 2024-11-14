using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateUserCommandHandler : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandHandler()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El Nombre no puede ser nulo");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("El Apellido no puede ser nulo");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("El Teléfono no puede ser nulo");
        }
    }
}
