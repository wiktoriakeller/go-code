using GoCode.Infrastructure.Identity.Entities;
using System.Security.Claims;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        Task<(string jwtToken, string jti)> CreateJwtToken(User user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
    }
}
