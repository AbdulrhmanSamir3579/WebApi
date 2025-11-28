namespace Api.Errors;

public class ApiResponse(int statusCode, string? message = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message ?? DefaultErrorMessage(statusCode);

    private static string DefaultErrorMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized Operation",
            404 => "Resource Not Found",
            500 => "Internal Server Error",
            _ => "Unknown Error"
        };
    }
}