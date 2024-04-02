namespace ISTUDIO.Domain.EntityModel;

public class FavoriteProductsEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}