﻿using GoCode.Application.Common.Contracts.Identity;
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
                    await _next(context);
                }
                else
                {
                    var contextResponse = context.Response;
                    contextResponse.ContentType = "application/json";
                    contextResponse.StatusCode = (int)response.HttpStatusCode;
                    var result = JsonSerializer.Serialize(response);
                    await contextResponse.WriteAsync(result);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class IdentityMiddlewareExtension
    {
        public static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<IdentityMiddleware>();
    }
}
