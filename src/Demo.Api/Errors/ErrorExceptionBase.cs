namespace Demo.Api.Errors;

public class ErrorExceptionBase(string code, string message = "") : Exception
{
    public string Code { get; set; } = code;

    public string Message { get; set; } = message;
}
