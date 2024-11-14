using Ecommerce.Application.Features.Adresses.VMS;

namespace Ecommerce.Application.Features.Auths.Users.VMS;

public class AuthResponse
{
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Telefono { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? Avatar { get; set; }

    public AddressVM? DireccionEnvio { get; set; }

    public ICollection<string>? Roles { get; set; }
}