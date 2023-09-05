namespace EmploymentSystem.WebApi.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDeafaultMessageForStatusCode(statusCode);
    }

    private string? GetDeafaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Not Authorized",
            404 => "Resource Not Found",
            500 => "Error Msg",
            _ => null
        };
    }
}
