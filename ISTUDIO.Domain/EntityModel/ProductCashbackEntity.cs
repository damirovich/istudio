namespace ISTUDIO.Domain.EntityModel;

public class ProductCashbackEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Связь с продуктом
    public decimal MaxBonusPercent { get; set; } // Максимальный процент использования бонусов для продукта

    // Навигационные свойства
    public  ProductsEntity Product { get; set; }
}
