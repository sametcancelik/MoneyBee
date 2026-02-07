using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace MoneyBee.Shared.Middlewares;

public class ApiKeyMiddleware(RequestDelegate _next, IConfiguration _configuration)
{
	private const string APIKEYNAME = "X-API-KEY";

	public async Task InvokeAsync(HttpContext context)
	{
		StringValues value;
		if (context.Request.Path.StartsWithSegments("/swagger"))
		{
			await _next(context);
		}
		else if (!context.Request.Headers.TryGetValue("X-API-KEY", out value))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("API Key bulunamadı.");
		}
		else if (!_configuration["Authentication:ApiKey"].Equals(value))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync("Geçersiz API Key.");
		}
		else
		{
			await _next(context);
		}
	}
}
