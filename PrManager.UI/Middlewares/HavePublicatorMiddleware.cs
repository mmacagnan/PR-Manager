using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PrManager.UI.Extensions;

namespace PrManager.UI.Middlewares
{
    /// <summary>
    /// Middleware to know if user have a publicator
    /// id. If don't and he try to login, we must
    /// take him to other page
    /// </summary>
    public class HaveCongregationMiddleware
    {
        private readonly RequestDelegate _next;

        public HaveCongregationMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (path.Contains("/admin/"))
            {
                var authenticated = context.User.Identity?.IsAuthenticated ?? false;
                if (authenticated)
                {
                    var currentUserPubId = context.User.Identity?.GetPublicatorId();
                    if (string.IsNullOrEmpty(currentUserPubId) || Convert.ToInt32(currentUserPubId) < 1)
                    {
                        context.Response.Redirect("/account/congregation-details");
                    }
                }
            }

            await _next(context);
        }
    }

    /// <summary>
    /// Extension for the middleware usage
    /// </summary>
    public static class UseHaveCongregationMiddlewareExtension
    {
        public static IApplicationBuilder UseHaveCongregationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HaveCongregationMiddleware>();
        }
    }
}