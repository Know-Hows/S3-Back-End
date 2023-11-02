namespace KnowHows_Back_End.Authentication
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var provideApiKey = context.Request.Headers[AuthConfig.ApiKeyHeader].FirstOrDefault();
            var isValid = IsValidApiKey(provideApiKey);

            if (!isValid)
            {
                await GenerateResponse(context, 401, "Invalid Authentication");
                return;
            }

            await _next(context);
        }

        private bool IsValidApiKey(string provideApiKey)
        {
            if (string.IsNullOrEmpty(provideApiKey))
            {
                return false;
            }

            var validApiKey = _configuration.GetValue<string>(AuthConfig.AuthSection);

            return string.Equals(validApiKey, provideApiKey, StringComparison.Ordinal);
        }

        private static async Task GenerateResponse(HttpContext context, int httpStatusCode, string msg)
        {
            context.Response.StatusCode = httpStatusCode;
            await context.Response.WriteAsync(msg);
        }
    }
}
