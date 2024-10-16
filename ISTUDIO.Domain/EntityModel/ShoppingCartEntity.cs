namespace ISTUDIO.Domain.EntityModel;

public class ShoppingCartEntity
{
    // Уникальный идентификатор корзины
    public int Id { get; set; }
    // Идентификатор пользователя, которому принадлежит корзина
    public string UserId { get; set; }
    public int QuantyProduct {  get; set; }
    public DateTime? CreateDate { get; set; }
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}