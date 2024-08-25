
namespace ISTUDIO.Domain.EntityModel;

public class MagazineEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoLogoURL { get; set; }
    public string UserId { get; set; }
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    public ICollection<ShoppingCartEntity> ShoppingCarts { get; set; } = new List<ShoppingCartEntity>();

}
