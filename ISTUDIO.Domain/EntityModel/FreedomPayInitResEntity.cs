namespace ISTUDIO.Domain.EntityModel;

public class FreedomPayInitResEntity
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string PaymentId { get; set; }
    public string RedirectUrl { get; set; }
    public string RedirectUrlType { get; set; }
    public string Salt { get; set; }
    public string Sig { get; set; }
    public string ResultUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
