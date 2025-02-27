namespace ISTUDIO.Domain.EntityModel;

public class PaymentMethodEntity
{
    // Уникальный идентификатор метода оплаты
    public int Id { get; set; }

    // Значение (название) метода оплаты
    public string Name { get; set; }

    // Идентификатор типа оплаты, к которому относится данный метод оплаты
    public int PaymentTypeId { get; set; }

    // Тип оплаты, к которому относится данный метод оплаты
    public PaymentTypeEntity PaymentType { get; set; }

    public bool IsAvailable { get; set; }
    public bool IsTechnicalBreak { get; set; }

    // Связь с OrderPaymentEntity
    public ICollection<OrderPaymentEntity> OrderPayments { get; set; } = new List<OrderPaymentEntity>();
}