using GoCode.Domain.Interfaces;

namespace GoCode.Application.Common.Contracts.Identity
{
    public interface ICurrentUserService
    {
        IUser? User { get; }

        Task<bool> AddXP(int XP, string userId);
    }
}
