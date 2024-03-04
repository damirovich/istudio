namespace ISTUDIO.Domain.EntityModel;

public class PaymentMethodEntity
{
    // Уникальный идентификатор метода оплаты
    public int Id { get; set; }

    // Значение (название) метода оплаты
    public string Value { get; set; }

    // Идентификатор пользователя, связанного с данным методом оплаты
    public string UserId { get; set; }

    // Идентификатор типа оплаты, к которому относится данный метод оплаты
    public int PaymentTypeId { get; set; }

    // Тип оплаты, к которому относится данный метод оплаты
    public PaymentTypeEntity PaymentType { get; set; }
}