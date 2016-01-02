using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TKDemoMVC.Middleware {
    public class MyLoggingMiddleware {
        private readonly RequestDelegate _next;
        public MyLoggingMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext) {
            //Problems with json etc..
            if (httpContext.Request.Path.Value.Contains("api") ||
                httpContext.Request.Path.Value.Contains("TK")
                ) {
                await _next(httpContext);
                return;
            }
            var sw = new Stopwatch();
            sw.Start();

            using (var memoryStream = new MemoryStream()) {
                var bodyStream = httpContext.Response.Body;
                httpContext.Response.Body = memoryStream;

                await _next(httpContext);

                long elapsed = sw.ElapsedMilliseconds;
                Debug.WriteLine($"{httpContext.Request.Path}: {elapsed} ");
                var isHtml = httpContext.Response.ContentType?.ToLower().Contains("text/html");
                //TODO: coś nie działa
                await _next(httpContext);
                return;
                if (httpContext.Response.StatusCode == 200 && isHtml.GetValueOrDefault()) {
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        using (var streamReader = new StreamReader(memoryStream)) {
                            var responseBody = await streamReader.ReadToEndAsync();
                            var newFooter = $"<footer><hr/><div id=\"elapsed\">Elapsed: {elapsed} ms.</div>";
                            responseBody = responseBody.Replace("<footer>", string.Format(newFooter, sw.ElapsedMilliseconds));
                            httpContext.Response.Headers.Add("X-ElapsedMs", new[] { sw.ElapsedMilliseconds.ToString() });
                            using (var amendedBody = new MemoryStream())
                            using (var streamWriter = new StreamWriter(amendedBody)) {
                                streamWriter.Write(responseBody);
                                amendedBody.Seek(0, SeekOrigin.Begin);
                                await amendedBody.CopyToAsync(bodyStream);
                            }
                        }
                    }
                }
            }
        }
    }
    public static partial class BuilderExtensions {
        public static IApplicationBuilder UseMyLoggingMiddleware(
          this IApplicationBuilder builder) {
            return builder.UseMiddleware<MyLoggingMiddleware>();
        }
    }
}
