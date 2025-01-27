namespace ISTUDIO.Domain.EntityModel;
// Модель для хранения информации об оплате заказа
public class OrderPaymentEntity
{
    // Уникальный идентификатор оплаты
    public int Id { get; set; }

    // Идентификатор заказа
    public int OrderId { get; set; }
    public OrderEntity Order { get; set; }

    // Метод оплаты
    public int PaymentMethodId { get; set; }
    public PaymentMethodEntity PaymentMethod { get; set; }

    // Сумма оплаты
    public decimal Amount { get; set; }

    // Статус оплаты (например, Paid, Pending, Failed)
    public string Status { get; set; }

    // Дата совершения оплаты
    public DateTime PaymentDate { get; set; }

    // Идентификатор транзакции (например, из платёжной системы)
    public string? TransactionId { get; set; }
}
