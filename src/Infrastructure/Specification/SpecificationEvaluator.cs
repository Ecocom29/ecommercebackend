using Ecommerce.Application.Specifications;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Specification;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria);
        }

        if (spec.Orderby != null)
        {
            inputQuery = inputQuery.OrderBy(spec.Orderby);
        }

        if (spec.OrderByDecending != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderByDecending);
        }

        if (spec.IsPaginEnable)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }

        inputQuery = spec.Includes!
                .Aggregate(inputQuery, (current, include) => current.Include(include).AsSplitQuery().AsNoTracking());


        return inputQuery;
    }
}