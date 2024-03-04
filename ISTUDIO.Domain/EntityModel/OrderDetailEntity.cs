namespace ISTUDIO.Domain.EntityModel;

public class OrderDetailEntity
{
    // Уникальный идентификатор детали заказа
    public int Id { get; set; }

    // Цена за единицу продукта в данной детали заказа
    public decimal UnitPrice { get; set; }

    // Количество продуктов в данной детали заказа
    public int Quantity { get; set; }

    // Идентификатор заказа, к которому относится данная деталь
    public int OrderId { get; set; }

    // Идентификатор продукта, связанного с данной деталью заказа
    public int ProductId { get; set; }

    // Заказ, к которому относится данная деталь
    public OrderEntity Order { get; set; }

    // Продукт, связанный с данной деталью заказа
    public ProductsEntity Product { get; set; }
}
