using GoCode.Infrastructure.Identity.Dto;
using GoCode.Infrastructure.Identity.Entities;
using System.Security.Claims;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        Task<JwtTokenInfoDto> CreateJwtToken(User user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token, bool validateLifetime);
    }
}
