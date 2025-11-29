namespace Api.Errors;

public class ErrorResponse(int statusCode, string message, List<string>? errors = null)
{
    public int StatusCode { get; set; } = statusCode;

    public string Message { get; set; } = message;

    public List<string>? Errors { get; set; } = errors?.Any() == true ? errors : null;
}