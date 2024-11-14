using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre no puede ser nulo");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("El apellido no puede ser nulo");
        }
    }
}
