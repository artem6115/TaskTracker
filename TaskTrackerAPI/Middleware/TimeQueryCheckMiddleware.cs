using Infrastructure.Auth;

namespace TaskTrackerAPI.Middleware
{
    public class TimeQueryCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeQueryCheckMiddleware> _logger;

        public TimeQueryCheckMiddleware(RequestDelegate next, ILogger<TimeQueryCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var time = DateTime.Now;
            await _next(context);
            _logger.LogCritical($"Время обработки запроса - {(DateTime.Now-time)}");
        }
    }
}
