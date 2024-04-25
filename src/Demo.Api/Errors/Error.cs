namespace Demo.Api.Errors;

public static class Error
{
    public static AuthorizationErrorException Authorization(string code, string message = "") => new(code, message);

    public static NotFoundErrorException NotFound(string code, string message = "") => new(code, message);

    public static UnexpectedErrorException Unexpected(string code, string message = "") => new(code, message);

    public static ValidationErrorException Validation(string code, string message = "") => new(code, message);
}
