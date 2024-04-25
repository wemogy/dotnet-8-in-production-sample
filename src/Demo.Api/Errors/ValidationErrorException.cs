namespace Demo.Api.Errors;

public class ValidationErrorException(string code, string message = "") : ErrorExceptionBase(code, message)
{
}
