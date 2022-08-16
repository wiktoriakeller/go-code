using GoCode.Application.Common.Contracts.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;

namespace GoCode.WebAPI.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IIdentityService identityService)
        {
            var token = await context.GetTokenAsync("access_token");

            if (token is not null)
            {
                var response = await identityService.GetUserFromTokenAsync(token);

                if (response.Succeeded)
                {
                    context.Items["User"] = response.Data;
                }
            }

            await _next(context);
        }
    }
}
