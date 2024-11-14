using AutoMapper;
using Ecommerce.Application.Features.Countries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, IReadOnlyList<CountryVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCountryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CountryVM>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countries = await _unitOfWork.Repository<Country>().GetAsync(
            null,
            x => x.OrderBy(y => y.Name),
            string.Empty,
            false
            );

            return _mapper.Map<IReadOnlyList<CountryVM>>(countries);


        }
    }
}
