using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayConfirmOperReqModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("otp")]
    public string Otp { get; set; }
}
