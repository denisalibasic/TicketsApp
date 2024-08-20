namespace TicketsAPI.authentication
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyHeaderName = "X-API-Key";
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidator apiKeyValidator)
        {
            _next = next;
            _apiKeyValidator = apiKeyValidator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) && _apiKeyValidator.IsValid(apiKey))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
            }
        }
    }
}
