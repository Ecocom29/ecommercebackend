using System.Linq.Expressions;
using MailKit.Search;

namespace Ecommerce.Application.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>>? Orderby { get; private set; }

    public Expression<Func<T, object>>? OrderByDecending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPaginEnable { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        Orderby = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDecending = orderByExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPaginEnable = true;
    }

    protected void AddInclute(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}