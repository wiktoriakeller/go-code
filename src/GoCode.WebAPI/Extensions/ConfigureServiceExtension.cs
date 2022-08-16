using GoCode.Application.Contracts.Identity;
using GoCode.WebAPI.Services;

namespace GoCode.WebAPI.Extensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
