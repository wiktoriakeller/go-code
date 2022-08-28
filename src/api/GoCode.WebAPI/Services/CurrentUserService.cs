using GoCode.Application.Common.Contracts.Identity;
using GoCode.Domain.Interfaces;

namespace GoCode.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
    }
}
