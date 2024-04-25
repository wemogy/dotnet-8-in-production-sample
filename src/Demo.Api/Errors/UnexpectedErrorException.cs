namespace Demo.Api.Errors;

public class UnexpectedErrorException : ErrorExceptionBase
{
    public UnexpectedErrorException(string code, string message = "")
        : base(code, message)
    {
    }
}
