namespace ISTUDIO.Domain.EntityModel;

public class OrderStatusHistoryEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Status { get; set; }
    public DateTime ChangeDate { get; set; }

    public OrderEntity Order { get; set; }
}
