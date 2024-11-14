using Ecommerce.Application.Features.Reviews.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<ReviewVM>
    {
        public int ProductId { get; set; }
        public string? Nombre { get; set; }
        public int Rating { get; set; }
        public string? Comentario { get; set; }
    }
}
