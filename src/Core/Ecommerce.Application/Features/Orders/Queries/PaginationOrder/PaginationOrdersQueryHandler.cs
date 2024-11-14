using AutoMapper;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Orders;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain;
using MediatR;
using Stripe;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrder
{
    public class PaginationOrdersQueryHandler : IRequestHandler<PaginationOrdersQuery, PaginationVM<OrderVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVM<OrderVM>> Handle(PaginationOrdersQuery request, CancellationToken cancellationToken)
        {
            var OrderSpecificationParams = new OrderSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                Id = request.Id,
                UserName = request.UserName
            };

            var spect = new OrderSpecification(OrderSpecificationParams);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpec(spect);

            var spectCount = new OrderForCountingSpecification(OrderSpecificationParams);

            var totalOrders = await _unitOfWork.Repository<Order>().CountAsync(spectCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalOrders) / Convert.ToDecimal(request.PageSize));

            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderVM>>(orders);

            var productsByPage = orders.Count();

            var pagination = new PaginationVM<OrderVM>
            {
                Count = totalOrders,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage
            };

            return pagination;
        }
    }
}
