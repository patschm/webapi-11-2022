using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Middleware
{
    public class HeenEnweerMiddleware
    {
        private readonly RequestDelegate _next;

        public HeenEnweerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            System.Console.WriteLine("Heenweg");
            await _next(ctx);
            System.Console.WriteLine("Terugweg");
        }
    }

    public static class HEWExtensions
    {
        public static void UseHeenEnWeer(this IApplicationBuilder bld)
        {
            bld.UseMiddleware<HeenEnweerMiddleware>();
        }
    }
}