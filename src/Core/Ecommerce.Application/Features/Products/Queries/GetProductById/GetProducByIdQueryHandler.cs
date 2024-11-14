using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetProductById;

public class GetProducByIdQueryHandler : IRequestHandler<GetProducByIdQuery, ProductVM>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProducByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Busqueda de producto por ID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna el objeto product</returns>
    public async Task<ProductVM> Handle(GetProducByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Product, object>>>();
        includes.Add(p => p.Images!);
        includes.Add(p => p.Reviews!.OrderByDescending(x => x.CreatedDate));

        var product = await _unitOfWork.Repository<Product>().GetEntityAsync(
            x => x.Id == request.ProductId,
            includes,
            true
        );

        return _mapper.Map<ProductVM>(product);
    }
}