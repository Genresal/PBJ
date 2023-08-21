using Serilog;

namespace PBJ.StoreManagementService.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information($"Sending request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            Log.Information($"Sending response: {context.Response.StatusCode}");
        }
    }
}
