using AutoMapper;
using Azure.Core;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts;


public class PaginationProductsQueryHandle : IRequestHandler<PaginationProductsQuery, PaginationVM<ProductVM>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationProductsQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVM<ProductVM>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
    {
        var ProductSpecificationParams = new ProductSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            CatetegoryId = request.CategoryId,
            PrecioMax = request.PrecioMax,
            PrecioMin = request.PrecioMin,
            Rating = request.Rating,
            Status = request.Status,
        };

        var spect = new ProductSpecification(ProductSpecificationParams);

        var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spect);

        var spectCount = new ProductForCountingSpecification(ProductSpecificationParams);

        var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(spectCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize)); //Redondear el valor

        var totalPages = Convert.ToInt32(rounded); //Obtener un numero entero para la paginación

        var data = _mapper.Map<IReadOnlyList<ProductVM>>(products); //Convertir los datos a tipo ProductoVM

        var productsByPage = products.Count();

        var pagination = new PaginationVM<ProductVM>
        {
            Count = totalProducts,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = productsByPage
        };

        return pagination;
    }
}