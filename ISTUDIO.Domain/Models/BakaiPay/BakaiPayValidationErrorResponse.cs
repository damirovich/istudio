using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayValidationErrorResponse
{
    [JsonPropertyName("detail")]
    public List<ValidationErrorDetail> Detail { get; set; }
}
public class ValidationErrorDetail
{
    [JsonPropertyName("loc")]
    public List<string> Loc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class ValidBadRequest
{
    [JsonPropertyName("detail")]
    public string Details { get; set; }
}

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}

public class UnprocessableEntityException : ApiException
{
    public UnprocessableEntityException(string message) : base(message, 422) { }
}

public class BadRequestExceptionBakai : ApiException
{
    public BadRequestExceptionBakai(string message) : base(message, 400) { }
}

public class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message, 404) { }
}

public class ValidBadRequestAPI
{
    [JsonPropertyName("Error")]
    public string Error { get; set; }
}

