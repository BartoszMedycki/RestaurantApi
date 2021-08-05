using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestaurantApiDataBase.Exceptions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class TimeRequestMiddleware : IMiddleware
    {
        private readonly ILogger<TimeRequestMiddleware> _logger;

        public TimeRequestMiddleware(ILogger<TimeRequestMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread.Sleep(5000);
            await next.Invoke(context);
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 4000)
            {
                string msng = $"Request [{context.Request.Method}] at {context.Request.Path} took  {stopwatch.ElapsedMilliseconds}";
                _logger.LogWarning(msng);
            }
        }
    }
}