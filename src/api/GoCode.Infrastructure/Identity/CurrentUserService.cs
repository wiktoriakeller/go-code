using GoCode.Application.Common.Contracts.Identity;
using GoCode.Domain.Interfaces;
using GoCode.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GoCode.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private const int LevelTresholdXP = 200;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public IUser? User => GetCurrentUser();

        private IUser? GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Items.ContainsKey("User"))
            {
                return _httpContextAccessor.HttpContext?.Items["User"] as IUser;
            }

            return null;
        }

        public async Task<bool> AddXP(int XP, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentXP = user.TotalXP + XP;
            var levelUp = false;

            user.TotalXP = currentXP;

            if (currentXP >= LevelTresholdXP)
            {
                user.Level += 1;
                user.TotalXP = currentXP - LevelTresholdXP;
                levelUp = true;
            }

            await _userManager.UpdateAsync(user);
            return levelUp;
        }
    }
}
