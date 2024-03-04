namespace ISTUDIO.Domain.EntityModel;

public class ShoppingCartEntity
{
    // Уникальный идентификатор корзины
    public int Id { get; set; }

    // Идентификатор пользователя, которому принадлежит корзина
    public string UserId { get; set; }

    // Список деталей заказа в корзине
    public ICollection<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();
}