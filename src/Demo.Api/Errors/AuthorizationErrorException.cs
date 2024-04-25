namespace Demo.Api.Errors;

public class AuthorizationErrorException : ErrorExceptionBase
{
    public AuthorizationErrorException(string code, string message = "")
        : base(code, message)
    {
    }
}
