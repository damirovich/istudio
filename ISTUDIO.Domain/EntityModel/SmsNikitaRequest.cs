namespace ISTUDIO.Domain.EntityModel;

public class SmsNikitaRequest
{
    public Guid Id { get; set; }
    public string? SenderCompany { get; set; }
    public string? TextSms { get; set; }
    public DateTime? Time { get; set; }
    public string? PhonesNumber { get; set; }
    public byte? Test { get; set; }

    public IList<SmsNikitaResponse> Responses { get; set; } = new List<SmsNikitaResponse>();
}
