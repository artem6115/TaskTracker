using System.Text;

namespace TaskTrackerAPI.Middleware
{
    public class ExceptionMeddleware 
    {
        private readonly RequestDelegate _next;


        public ExceptionMeddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccessViolationException ex) {
                context.Response.StatusCode = 401;
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(ex.Message ?? "Access denied"));
            }
            catch(FileNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(ex.Message ?? "Resource not found"));
            }
            
        }
    }
}
