using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Models.Token;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    public JwtSettings _jwtSettings { get; }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccesor, IOptions<JwtSettings> jwtSettings)
    {
        _httpContextAccessor = httpContextAccesor;
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(Usuario usuario, IList<string>? roles)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!),
            new Claim("userId", usuario.Id),
            new Claim("email", usuario.Email!)
        };

        foreach (var rol in roles!)
        {
            var claim = new Claim(ClaimTypes.Role, rol);
            claims.Add(claim);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
            SigningCredentials = credenciales
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public string GetSessionUser()
    {
        var username = _httpContextAccessor.HttpContext!.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        return username!;
    }
}