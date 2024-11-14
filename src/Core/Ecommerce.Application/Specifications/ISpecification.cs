
using System.Linq.Expressions;

namespace Ecommerce.Application.Specifications;


public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

    List<Expression<Func<T, object>>>? Includes { get; }

    Expression<Func<T, object>>? Orderby { get; }

    Expression<Func<T, object>>? OrderByDecending { get; }

    int Take { get; }

    int Skip { get; }

    bool IsPaginEnable { get; }
}