namespace ISTUDIO.Domain.EntityModel
{
    // Заказ, сделанный пользователем
    public class OrderEntity
    {
        // Уникальный идентификатор заказа
        public int Id { get; set; }

        // Адрес доставки для этого заказа
        public string ShippingAddress { get; set; }

        // Общая цена заказа
        public decimal TotalPrice { get; set; }

        // Общее количество продуктов в заказе
        public int TotalQuantyProduct { get; set; }

        // Статус доставки заказа
        public string Status { get; set; }

        public DateTime CreateDate { get; set; }
        public string PaymentMethod { get; set; }

        public string? ReceiptPhoto { get; set; }

        // Идентификатор пользователя, сделавшего этот заказ
        public string UserId { get; set; }

        public int? InvoiceId { get; set; }
        public InvoiceEntity Invoice { get; set; }

        // Убираем коллекцию магазинов, так как связь с магазинами идет через OrderDetailEntity
        // public ICollection<MagazineEntity> Magazines { get; set; } = new List<MagazineEntity>();

        // Коллекция продуктов, связанных с заказом (через детали заказа)
        public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();

        // Коллекция деталей заказа
        public ICollection<OrderDetailEntity> Details { get; set; } = new HashSet<OrderDetailEntity>();

        // Связь с покупателями
        public ICollection<CustomersEntity> Customers { get; set; } = new List<CustomersEntity>();

        // Связь с адресами доставки
        public ICollection<OrderAddressEntity> OrderAddresses { get; set; } = new List<OrderAddressEntity>();

        // История изменения статусов заказа
        public ICollection<OrderStatusHistoryEntity> StatusHistories { get; set; } = new List<OrderStatusHistoryEntity>();
    }
}
