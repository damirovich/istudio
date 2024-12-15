using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayCreateOperationReqModel
{
    [JsonPropertyName("payment_code")]
    public string? PaymentCode { get; set; }

    [JsonPropertyName("phone")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }
}
