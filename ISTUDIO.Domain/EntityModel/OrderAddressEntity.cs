
namespace ISTUDIO.Domain.EntityModel;

public class OrderAddressEntity
{
    public int Id { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public string? UserId { get; set; }

    public int? OrderId { get; set; }
    public OrderEntity? Orders { get; set; }
}
