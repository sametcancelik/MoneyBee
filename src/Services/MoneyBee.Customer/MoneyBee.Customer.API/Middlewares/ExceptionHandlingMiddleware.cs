using System.Net;
using System.Text.Json;
using FluentValidation;

namespace MoneyBee.Customer.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
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
        var code = HttpStatusCode.InternalServerError;
        object responsePayload;

        if (exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            var errorMessages = validationException.Errors?
                                .Select(e => e.ErrorMessage)
                                .ToList()
                                ?? new List<string> { exception.Message };

            responsePayload = new { errors = errorMessages };
        }
        else
        {
            responsePayload = new { error = "Beklenmedik bir hata oluştu.", detail = exception.Message };
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(JsonSerializer.Serialize(responsePayload));
    }
}