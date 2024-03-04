namespace ISTUDIO.Domain.EntityModel;

public class PaymentTypeEntity
{
    // Уникальный идентификатор типа оплаты
    public int Id { get; set; }

    // Название типа оплаты
    public string Name { get; set; }

    // Коллекция методов оплаты, связанных с данным типом оплаты
    public ICollection<PaymentMethodEntity> PaymentMethods { get; set; }
}
