using Infrastructure.Auth;

namespace TaskTrackerAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;   
        public AuthMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                var user = new UserClaims()
                {
                    Id = long.Parse(context.User.Claims.First(x => x.Type == "Id").Value),
                    Email = context.User.Claims.First(x => x.Type == "Email").Value,
                    FullName = context.User.Claims.First(x => x.Type == "FullName").Value
                };
                UserClaims.User = user;
            }
            catch { }
            await _next(context);
        }
    }
}
