using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKDemoMVC.Middleware
{
    public class MyProcessMiddleware
    {
        private readonly RequestDelegate _next;
        public MyProcessMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext) {
            if (httpContext.Request.Headers.ContainsKey("X-Error")) {
                await httpContext.Response.WriteAsync("ERROR");
            } else {
                await _next.Invoke(httpContext);
            }
        }
    }
    public static partial class BuilderExtensions {
        public static IApplicationBuilder UseMyProcessMiddleware(
          this IApplicationBuilder builder) {
            return builder.UseMiddleware<MyProcessMiddleware>();
        }
    }
}
