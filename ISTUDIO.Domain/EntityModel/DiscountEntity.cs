namespace ISTUDIO.Domain.EntityModel;

public class DiscountEntity
{
    // Уникальный идентификатор скидки
    public int Id { get; set; }

    // Величина скидки в процентах
    public decimal PercenTage { get; set; } 

    // Время начала действия скидки
    public DateTime StartTime { get; set; }

    // Время окончания действия скидки
    public DateTime EndTime { get; set; }

    // Продукты данной скидки
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}