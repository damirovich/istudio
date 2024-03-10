using System.Xml.Serialization;

namespace ISTUDIO.Domain.EntityModel;

public class SmsNikitaResponse
{
    public int Id { get; set; }
    public int? Phones { get; set; }
    public int? SmsCount { get; set; }
    public string? Message { get; set; }
    public Guid SmsRequestId { get; set; }
    public int SmsStatusId { get; set; }
    public SmsNikitaRequest Request { get; set; }
    public SmsNikitaStatus SmsStatus { get; set; }
}
