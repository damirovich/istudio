using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models.BakaiPay;

public class BakaiPayCheckStatusResModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("payment_code")]
    public string PaymentCode { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    [JsonPropertyName("confirmed_at")]
    public string ConfirmedAt { get; set; }

    [JsonPropertyName("err_msg")]
    public string ErrMsg { get; set; }
}
