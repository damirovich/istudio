namespace ISTUDIO.Domain.EntityModel;

public class BakaiConfirmTranResEntity
{
    public int Id { get; set; }
    public int CreateTranId { get; set; }
    public string Status { get; set; }
    public string OrderId { get; set; }
}
