using FluentValidation;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            //Validaciones
            RuleFor(p => p.Nombre)
                .NotNull().WithMessage("El Nombre no permite valores nulos.");

            RuleFor(p => p.Comentario)
                .NotNull().WithMessage("El Comentario no permite valores nulos.");

            RuleFor(p => p.Rating)
                .NotNull().WithMessage("El Rating no permite valores nulos.");
        }
    }
}
