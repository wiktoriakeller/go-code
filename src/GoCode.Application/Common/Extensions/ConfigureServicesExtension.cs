using FluentValidation;
using GoCode.Application.Common.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoCode.Application.Common.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ConfigureServicesExtension).Assembly);
            services.AddMediatR(typeof(ConfigureServicesExtension).Assembly);

            //Validators
            services.AddValidatorsFromAssembly(typeof(ConfigureServicesExtension).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            return services;
        }
    }
}
