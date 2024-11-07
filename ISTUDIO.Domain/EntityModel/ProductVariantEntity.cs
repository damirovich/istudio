namespace ISTUDIO.Domain.EntityModel;

public class ProductVariantEntity
{
    // Уникальный идентификатор варианта продукта
    public int Id { get; set; }

    // Идентификатор продукта, к которому относится вариант
    public int? ProductId { get; set; }

    // Связь с продуктом
    public ProductsEntity Product { get; set; }

    // Цена для данной комбинации атрибутов
    public decimal Price { get; set; }

    // Количество на складе для данной вариации
    public int? QuantityInStock { get; set; }

    // Список атрибутов для этого варианта
    public ICollection<ProductAttributeEntity> Attributes { get; set; } = new List<ProductAttributeEntity>();
}
