using Ecommerce.Application.Features.Categories.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoryListQuery: IRequest<IReadOnlyList<CategoryVM>>
    {

    }
}
