using System.Text.Json.Serialization;

namespace ISTUDIO.Contracts.Features.BakaiPay;

public class BakaiConfirmPayVMy
{
    public int Id { get; set; }
    public string OTP { get; set; }
}

