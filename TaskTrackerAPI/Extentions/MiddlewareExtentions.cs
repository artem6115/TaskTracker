using TaskTrackerAPI.Middleware;

namespace TaskTrackerAPI.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AuthMiddleware>();
            return builder;
        }
        public static IApplicationBuilder UseQueryTimeCheckerMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<TimeQueryCheckMiddleware>();
            return builder;
        }
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMeddleware>();
            return builder;
        }

    }
}
