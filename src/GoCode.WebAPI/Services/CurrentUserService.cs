using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Identity.Dto;

namespace GoCode.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto? User => GetCurrentUser();

        private UserDto? GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Items.ContainsKey("User"))
            {
                return _httpContextAccessor.HttpContext?.Items["User"] as UserDto;
            }

            return null;
        }
    }
}
