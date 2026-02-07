using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;

namespace MoneyBee.Shared.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        int statusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            BusinessException ex => ex.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        ServiceResponse responseContent = exception switch
        {
            ValidationException ex => ServiceResponse.Failure(ex.Errors.Select(e => e.ErrorMessage).ToList(), statusCode),
            BusinessException ex => ServiceResponse.Failure(ex.Message, statusCode),
            _ => ServiceResponse.Failure($"Beklenmedik bir hata oluştu: {exception.InnerException?.Message ?? exception.Message ?? "Bilinmeyen hata"}", statusCode)
        };

        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            MaxDepth = 512
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(responseContent, options));
    }
}