using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Infrastructure.DataAccess;
using GoCode.Infrastructure.Identity;
using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using GoCode.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GoCode.Infrastructure.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Service registrattion
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IRepository<RefreshToken>, BaseRepositoryAsync<RefreshToken>>();

            //Register db
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ConfigureServicesExtension).Assembly.FullName)));

            //Register identity and jwt
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Authentication:JwtOptions:Issuer"],
                ValidAudience = configuration["Authentication:JwtOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtOptions:Key"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = tokenValidationParameters;
            });

            //Mapping profiles
            services.AddAutoMapper(typeof(ConfigureServicesExtension).Assembly);

            //Options bindings
            services.AddSingleton(tokenValidationParameters);

            services.Configure<JwtOptions>(configuration.GetSection("Authentication:JwtOptions"));

            return services;
        }
    }
}
