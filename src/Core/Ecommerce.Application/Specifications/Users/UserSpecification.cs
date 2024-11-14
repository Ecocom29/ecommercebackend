using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Users
{
    public class UserSpecification :BaseSpecification<Usuario>
    {
        public UserSpecification(UserSpecificationParams userparams) : base(
            x=> 
                (
                string.IsNullOrEmpty(userparams.Search) 
                || x.Nombre!.Contains(userparams.Search) 
                || x.Apellido!.Contains(userparams.Search)
                || x.Email!.Contains(userparams.Search)
                )
            )
        {
            ApplyPaging(userparams.PageSize * (userparams.PageIndex - 1), userparams.PageSize);


            if (!string.IsNullOrEmpty(userparams.Sort))
            {
                switch(userparams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p=> p.Nombre!);
                        break;
                    case "nombreDesc":
                        AddOrderBy(p => p.Nombre!);
                        break;
                    case "ApellidoAsc":
                        AddOrderBy(p => p.Apellido!);
                        break;
                    case "ApellidoDesc":
                        AddOrderBy(p => p.Apellido!);
                        break;
                    default:
                        AddOrderBy(p=>p.Nombre!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p=>p.Nombre!);
            }
        }
    }
}
