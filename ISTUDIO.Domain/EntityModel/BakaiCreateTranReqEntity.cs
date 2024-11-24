namespace ISTUDIO.Domain.EntityModel;

public class BakaiCreateTranReqEntity
{
    public int Id { get; set; }
    public string PaymentCode { get; set; }
    public string PhoneNumber { get; set; }
    public int Amount { get; set; }
    public string OrderId { get; set; }
}
