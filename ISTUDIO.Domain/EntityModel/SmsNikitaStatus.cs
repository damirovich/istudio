namespace ISTUDIO.Domain.EntityModel;

public class SmsNikitaStatus
{
    public int Id { get; set; }
    public int Status { get; set; }
    public string? Name { get; set; }

    public IList<SmsNikitaResponse> SmsNikitaResponses { get; set; }
}
