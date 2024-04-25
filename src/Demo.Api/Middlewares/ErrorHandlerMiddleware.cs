using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Demo.Api.Middlewares;

// Primary constructor (Introduced in C# 12)
public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsJsonAsync(exception.Message);
        }
    }
}
