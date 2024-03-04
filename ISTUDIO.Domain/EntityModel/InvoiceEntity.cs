namespace ISTUDIO.Domain.EntityModel;

public class InvoiceEntity
{
    // Уникальный идентификатор счета
    public int Id { get; set; }

    // Общая стоимость счета
    public decimal TotalValue { get; set; }

    // Дата выставления счета
    public DateTime DateIssued { get; set; }

    // Идентификатор пользователя, связанного с этим счетом
    public string UserId { get; set; }

    // Идентификатор заказа, связанного с этим счетом
    public int OrderId { get; set; }

    // Заказ, связанный с этим счетом
    public OrderEntity Order { get; set; }

    // Детали счета
    public ICollection<InvoiceDetailEntity> InvoiceDetails { get; set; } = new List<InvoiceDetailEntity>();
}