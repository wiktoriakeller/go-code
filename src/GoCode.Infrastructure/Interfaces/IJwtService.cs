using GoCode.Infrastructure.Authentication;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(ApplicationUser user);
    }
}