namespace Demo.Api.Errors;

public class NotFoundErrorException : ErrorExceptionBase
{
    public NotFoundErrorException(string code, string message = "")
        : base(code, message)
    {
    }
}
