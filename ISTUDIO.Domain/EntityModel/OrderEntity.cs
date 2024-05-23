namespace ISTUDIO.Domain.EntityModel;

// Заказ, сделанный пользователем
public class OrderEntity
{
    // Уникальный идентификатор заказа
    public int Id { get; set; }

    // Адрес доставки для этого заказа
    public string ShippingAddress { get; set; }

    // Общая цена заказа
    public decimal TotalPrice { get; set; }

    //Общая количество продуктов в заказе
    public int TotalQuantyProduct {  get; set; }

    // Статус доставки заказа
    public string Status { get; set; }

    public DateTime CreateDate { get; set; }

    // Идентификатор пользователя, сделавшего этот заказ
    public string UserId { get; set; }
    public int? InvoiceId { get; set; }
    public InvoiceEntity Invoice { get; set; }

    // Список продуктов в этом заказе
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();

    // Детали заказа
    public ICollection<OrderDetailEntity> Details { get; set; } = new HashSet<OrderDetailEntity>();
    public ICollection<CustomersEntity> Customers { get; set; } = new List<CustomersEntity>();
    public ICollection<OrderAddressEntity> OrderAddresses { get; set; } = new List<OrderAddressEntity>();
}
