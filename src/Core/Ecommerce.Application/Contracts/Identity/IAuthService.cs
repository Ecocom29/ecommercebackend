using Ecommerce.Domain;

namespace Ecommerce.Application.Identity;

public interface IAuthService
{
    string GetSessionUser();
    string CreateToken(Usuario usuario, IList<string>? roles);
}