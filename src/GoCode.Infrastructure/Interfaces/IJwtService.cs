using GoCode.Infrastructure.Identity.Entities;
using System.Security.Claims;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(ApplicationUser user);

        ClaimsPrincipal? GetPrincipalFromToken(string token);
    }
}