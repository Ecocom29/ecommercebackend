using AutoMapper;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrdersById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVM> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Order, object>>>();
            includes.Add(p => p.OrderItems!.OrderBy(x=>x.Product));
            includes.Add(p=>p.OrderAddress!);

            var order = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x=>x.Id == request.OrderId,
                includes,
                false
            );

            return _mapper.Map<OrderVM>( order );
        }
    }
}
