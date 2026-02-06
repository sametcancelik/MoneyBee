using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace MoneyBee.Shared.Middlewares;

public class ExceptionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Detailed = exception.InnerException?.Message 
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}