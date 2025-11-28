namespace Api.Errors;

public class ValidationErrorResponse() : ApiResponse(400)
{
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}