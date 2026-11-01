namespace UserManagement.Api.Common.Responses;

public static class ValidationResponse
{
    public static object Create(
        IDictionary<string, string[]> errors)
    {
        return new
        {
            Success = false,
            Message = "Validation failed.",
            Data = (object?)null,
            Errors = errors
        };
    }
}