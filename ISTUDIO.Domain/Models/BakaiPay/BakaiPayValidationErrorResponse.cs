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