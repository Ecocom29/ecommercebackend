using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain;

public class Usuario : IdentityUser {

    public string? Nombre {get;set;}

    public string? Apellido {get;set;}

    public string? Telefono { get; set; }

    public string? AvatarUrl { get; set; }

    public bool IsActive { get; set; } = true;

}