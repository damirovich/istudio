namespace ISTUDIO.Domain.EntityModel;

public class OrderStatusEntity
{
    public int Id { get; set; } // Уникальный идентификатор статуса

    public string NameRus { get; set; } // Название статуса (например, \"Новый\", \"Оплачен\")

    public string NameEng { get; set; }

    public string Description { get; set; } // Описание статуса (для админов или внутреннего использования)

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>(); // Связь с заказами
}
