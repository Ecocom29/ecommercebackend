using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList;


public class GetProductListQuery : IRequest<IReadOnlyList<ProductVM>> //El controller va devolder una lista de productos
{

}