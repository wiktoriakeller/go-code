using GoCode.Domain.Entities;

namespace GoCode.Application.Contracts
{
    public interface IJwtService
    {
        string CreateJwtToken(User user);
    }
}