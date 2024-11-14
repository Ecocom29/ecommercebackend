using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Orders
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(OrderSpecificationParams orderParams) : base(
                x => (string.IsNullOrEmpty(orderParams.UserName) || x.CompradorUsername!.Contains(orderParams.UserName))
                && (!orderParams.Id.HasValue || x.Id == orderParams.Id)
            )
        {
            AddInclute(p => p.OrderItems!);
            ApplyPaging(orderParams.PageSize * (orderParams.PageIndex - 1), orderParams.PageSize);

            if (!string.IsNullOrEmpty(orderParams.Sort))
            {
                switch (orderParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                    case "createDateDesc":
                        AddOrderByDescending(p => p.CreatedDate!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate!);
            }
        }
    }
}
