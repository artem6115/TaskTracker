using TaskTrackerAPI.Middleware;

namespace TaskTrackerAPI.Extentions
{
    public static class AuthMiddlewareExtentions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AuthMiddleware>();
            return builder;
        }
    }
}
