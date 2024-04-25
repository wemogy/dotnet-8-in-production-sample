using System.Net;
using Demo.Api.Errors;

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
        catch (ValidationErrorException exception)
        {
            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsJsonAsync(exception.Message);
        }
        catch (AuthorizationErrorException exception)
        {
            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.Forbidden;
            await response.WriteAsJsonAsync(exception.Message);
        }
    }
}
