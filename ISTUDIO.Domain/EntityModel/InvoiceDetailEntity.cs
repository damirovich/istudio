namespace ISTUDIO.Domain.EntityModel;

public class InvoiceDetailEntity
{
    // Уникальный идентификатор детали счета
    public int Id { get; set; }

    // Количество товаров в детали счета
    public int ItemQuantity { get; set; }

    // Цена за единицу товара в детали счета
    public decimal ItemPrice { get; set; }

    // Идентификатор продукта, связанного с данной деталью счета
    public int ProductId { get; set; }

    // Идентификатор счета, к которому относится данная деталь
    public int InvoiceId { get; set; }

    // Продукт, связанный с данной деталью счета
    public ProductsEntity Products { get; set; }

    // Счет, к которому относится данная деталь
    public InvoiceEntity Invoice { get; set; }
}