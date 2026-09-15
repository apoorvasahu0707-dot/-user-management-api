namespace UserApi.Middleware
{
    // Simple logging middleware: logs method, path, status code, and time taken
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var start = DateTime.UtcNow;
            var method = context.Request.Method;
            var path = context.Request.Path;

            await _next(context); // pass control to next middleware / endpoint

            var duration = DateTime.UtcNow - start;
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation(
                "{Method} {Path} responded {StatusCode} in {Duration}ms",
                method, path, statusCode, duration.TotalMilliseconds);
        }
    }

    // Extension method for cleaner registration in Program.cs
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
