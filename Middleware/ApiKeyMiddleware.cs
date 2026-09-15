namespace UserApi.Middleware
{
    // Basic API key authentication middleware.
    // Client must send header: X-Api-Key: mysecretkey123
    public class ApiKeyMiddleware
    {
        private const string API_KEY_HEADER = "X-Api-Key";
        private const string VALID_API_KEY = "mysecretkey123"; // demo only, move to config for real use

        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow Swagger UI to load without a key so it's easy to test
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key is missing.");
                return;
            }

            if (!VALID_API_KEY.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }

            await _next(context);
        }
    }

    public static class ApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
