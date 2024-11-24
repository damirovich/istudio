

using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayCreateOperationResModel
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("payment_code")]
    public string PaymentCode { get; set; }
}
