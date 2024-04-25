namespace Demo.Api.Errors;

public class ErrorExceptionBase : Exception
{
    public string Code { get; set; }

    public string Message { get; set; }

    public ErrorExceptionBase(string code, string message = "")
    {
        Code = code;
        Message = message;
    }
}
