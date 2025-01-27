namespace ISTUDIO.Domain.EntityModel;
// Модель для хранения информации о доставке заказа
public class OrderDeliveryEntity
{
    // Уникальный идентификатор доставки
    public int Id { get; set; }

    // Идентификатор заказа
    public int OrderId { get; set; }
    public OrderEntity Order { get; set; }

    // Метод доставки (например, Ылдам Экспресс, Айла Экспресс)
    public string DeliveryMethod { get; set; }

    // Статус доставки (например, Pending, In Transit, Delivered)
    public string Status { get; set; }

    // Трек-номер для отслеживания
    public string TrackingNumber { get; set; }

    // Предполагаемая дата доставки
    public DateTime? EstimatedDate { get; set; }

    // Фактическая дата доставки
    public DateTime? DeliveredDate { get; set; }

    // Адрес доставки
    public int OrderAddressId { get; set; }
    public OrderAddressEntity OrderAddress { get; set; }
}
