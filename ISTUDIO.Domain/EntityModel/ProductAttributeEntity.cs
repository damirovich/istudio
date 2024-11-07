
namespace ISTUDIO.Domain.EntityModel;

public class ProductAttributeEntity
{
    // Уникальный идентификатор атрибута
    public int Id { get; set; }

    // Название атрибута (например: Цвет, Размер, Память)
    public string? Name { get; set; }

    // Значение атрибута (например: Черный, L, 128 ГБ)
    public string? Value { get; set; }

    // Идентификатор варианта продукта
    public int? ProductVariantId { get; set; }

    // Связь с вариантом продукта
    public ProductVariantEntity ProductVariant { get; set; }
}
