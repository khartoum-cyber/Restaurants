
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class TimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<TimeLoggingMiddleware> _logger;

        public TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            await next.Invoke(context);

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation("Request [{Method}] {Path} took {ElapsedMilliseconds} ms",
                context.Request.Method,
                context.Request.Path,
                elapsedMs);
        }
    }
}
