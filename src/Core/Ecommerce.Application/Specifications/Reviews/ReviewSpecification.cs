using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Specifications.Reviews
{
    public class ReviewSpecification : BaseSpecification<Review>
    {
        public ReviewSpecification(ReviewSpecificationParams reviewParams) : base(
            x => (!reviewParams.ProductId.HasValue || x.ProductId == reviewParams.ProductId)
            )
        {
            ApplyPaging(reviewParams.PageSize * (reviewParams.PageIndex -1) , reviewParams.PageSize);


            if(string.IsNullOrEmpty(reviewParams.Sort))
            {
                switch (reviewParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(p=>p.CreatedDate!);
                        break;
                    case "createDataDesc":
                        AddOrderByDescending(p=>p.CreatedDate!);
                        break;
                    default:
                        AddOrderBy(p=>p.CreatedDate!); 
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p=>p.CreatedDate!);
            }
        }
    }
}
