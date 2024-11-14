using AutoMapper;
using Ecommerce.Application.Features.Categories.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IReadOnlyList<CategoryVM>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryVM>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().GetAsync(
                null,
                x => x.OrderBy(y => y.Nombre),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<CategoryVM>>(categories); //Transformar a tipo CategoryVM
        }
    }
}
