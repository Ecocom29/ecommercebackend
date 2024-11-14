using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductVM>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
       _unitOfWork = unitOfWork;
       _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductVM>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Product, object>>>();

        includes.Add(p => p.Images!);
        includes.Add(p => p.Reviews!);

        var products = await _unitOfWork.Repository<Product>().GetAsync(
            null,
            x => x.OrderBy(y => y.Nombre),
            includes,
            true
        );
        var productsVM = _mapper.Map<IReadOnlyList<ProductVM>>(products);

        return productsVM;
    }
}