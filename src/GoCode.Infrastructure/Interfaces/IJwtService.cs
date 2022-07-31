using GoCode.Infrastructure.Identity;

namespace GoCode.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(ApplicationUser user);
    }
}