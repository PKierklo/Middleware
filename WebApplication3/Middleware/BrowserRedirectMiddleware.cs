using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
namespace WebApplication3.Middleware
{
    public class BrowserRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"].ToString();

            if (userAgent.Contains("Edge") || userAgent.Contains("Edg") || userAgent.Contains("IE") || userAgent.Contains("Opera GX"))
            {
                context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class BrowserRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseBrowserRedirect(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BrowserRedirectMiddleware>();
        }
    }
}


