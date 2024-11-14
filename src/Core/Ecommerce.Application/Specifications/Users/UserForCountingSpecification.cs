using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Users
{
    public class UserForCountingSpecification : BaseSpecification<Usuario>
    {
        public UserForCountingSpecification(UserSpecificationParams userparams) : base(
            x =>
                (
                string.IsNullOrEmpty(userparams.Search)
                || x.Nombre!.Contains(userparams.Search)
                || x.Apellido!.Contains(userparams.Search)
                || x.Email!.Contains(userparams.Search)
                )
            )
        {
            
        }
    }
}
