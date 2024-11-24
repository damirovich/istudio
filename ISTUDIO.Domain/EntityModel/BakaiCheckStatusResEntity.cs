namespace ISTUDIO.Domain.EntityModel;

public class BakaiCheckStatusResEntity
{
    public int Id { get; set; }
    public int CreateTranId { get; set; }
    public string PaymentCode { get; set; }
    public string Status { get; set; }
    public string OrderId { get; set; }
    public string ConfirmedAt { get; set; }
    public string ErrMsg { get; set; }
}
