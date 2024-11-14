using AutoMapper;
using Ecommerce.Application.Features.Reviews.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Reviews;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Queries.PaginationReviews
{
    public class PaginationReviewsQueryHandler : IRequestHandler<PaginationReviewsQuery, PaginationVM<ReviewVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVM<ReviewVM>> Handle(PaginationReviewsQuery request, CancellationToken cancellationToken)
        {
            //Setear
            var reviewSpecification = new ReviewSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                ProductId = request.ProductId
            };

            //Instanciar la clase
            var spec = new ReviewSpecification(reviewSpecification);
            var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpec(spec);

            var specCount = new ReviewForCountingSpecification(reviewSpecification);
            var totalReviews = await _unitOfWork.Repository<Review>().CountAsync(spec);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalReviews) / Convert.ToDecimal(request.PageSize));

            var totalPage = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewVM>>(reviews);

            var productByPage = reviews.Count();

            var pagination = new PaginationVM<ReviewVM>
            {
                Count = totalReviews,
                Data = data,
                PageCount = totalPage,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productByPage
            };

            return pagination;
        }
    }
}
