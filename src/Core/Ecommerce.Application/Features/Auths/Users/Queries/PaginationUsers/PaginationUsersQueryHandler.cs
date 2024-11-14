using CloudinaryDotNet.Actions;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Users;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers
{
    public class PaginationUsersQueryHandler : IRequestHandler<PaginationUsersQuery, PaginationVM<Usuario>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaginationUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationVM<Usuario>> Handle(PaginationUsersQuery request, CancellationToken cancellationToken)
        {
            var userSpecificationParams = new UserSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
            };

            var specification = new UserSpecification(userSpecificationParams);

            var users = await _unitOfWork.Repository<Usuario>().GetAllWithSpec(specification);

            var specCount = new UserForCountingSpecification(userSpecificationParams);
            
            var totalUsers = await _unitOfWork.Repository<Usuario>().CountAsync(specCount);

            var rounded = Math.Ceiling( Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
            
            var totalPages = Convert.ToInt32(rounded);

            var userByPage = users.Count();

            var pagination = new PaginationVM<Usuario>
            {
                Count = totalUsers,
                Data = users,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = userByPage
            };

            return pagination;
        }
    }
}
