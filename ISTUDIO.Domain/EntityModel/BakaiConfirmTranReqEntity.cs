namespace ISTUDIO.Domain.EntityModel;

public class BakaiConfirmTranReqEntity
{
    public int Id { get; set; }
    public int CreateTranId { get; set; }
    public string OTPCode { get; set; }
}
