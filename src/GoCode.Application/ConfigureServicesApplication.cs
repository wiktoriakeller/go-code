using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoCode.Application
{
    public static class ConfigureServicesApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ConfigureServicesApplication).Assembly);
            return services;
        }
    }
}