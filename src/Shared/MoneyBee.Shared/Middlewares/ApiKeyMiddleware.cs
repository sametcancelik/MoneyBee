using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MoneyBee.Shared.Middlewares;

public class ApiKeyMiddleware(RequestDelegate _next, IConfiguration _configuration)
{
    private const string APIKEYNAME = "X-API-KEY";

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key bulunamadı.");
            return;
        }

        var apiKey = _configuration["Authentication:ApiKey"];
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Geçersiz API Key.");
            return;
        }

        await _next(context);
    }
}