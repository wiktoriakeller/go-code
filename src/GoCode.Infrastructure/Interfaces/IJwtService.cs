using GoCode.Infrastructure.Identity.Entities;
using System.Security.Claims;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        (string jwtToken, string jti) CreateJwtToken(ApplicationUser user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
    }
}
