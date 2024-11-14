using FluentValidation;

namespace Ecommerce.Application.Features.Adresses.Command.CreateAddress
{
    public class CreateAddressCommandValidator: AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(p => p.Direccion)
                .NotEmpty().WithMessage("Tu dirección no puede ser vacia.");

            RuleFor(p => p.Ciudad)
                .NotEmpty().WithMessage("La Ciudad no puede ser vacia.");

            RuleFor(p => p.Departamento)
                .NotEmpty().WithMessage("El departamento no puede ser vacia.");

            RuleFor(p => p.CodigoPostal)
                .NotEmpty().WithMessage("El Código Postal no puede ser vacio.");

            RuleFor(p => p.Pais)
               .NotEmpty().WithMessage("El País no puede ser vacio.");
        }

    }
}
