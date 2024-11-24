using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayCheckPropsResModel
{
    [JsonPropertyName("message")]
    public bool Message { get; set; }
}
