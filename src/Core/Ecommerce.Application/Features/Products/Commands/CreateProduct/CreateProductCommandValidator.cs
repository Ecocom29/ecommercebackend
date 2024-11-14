using FluentValidation;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no puede estar en blanco")
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder de los 50 caracteres.");

            RuleFor(p => p.Descripcion)
                .NotEmpty()
                .WithMessage("La descripción no puede estás vaciá.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .WithMessage("El stock no puede ser nulo.");

            RuleFor(p => p.Precio)
                .NotEmpty()
                .WithMessage("El precio no puede ser nulo.");
        }
    }
}
