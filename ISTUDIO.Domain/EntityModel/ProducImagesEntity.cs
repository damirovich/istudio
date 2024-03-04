namespace ISTUDIO.Domain.EntityModel;

// Изображения продуктов
public class ProducImagesEntity
{
    // Уникальный идентификатор изображения продукта
    public int Id { get; set; }

    // Тип изображения (например, основное, дополнительное и т. д.)
    public string? TypeImg { get; set; }

    // Ссылка на изображение
    public string? Url { get; set; }

    // Наименование изображения (если применимо)
    public string? Name { get; set; }

    // Тип содержимого изображения (например, image/jpeg, image/png и т. д.)
    public string? ContentType { get; set; }

    // Идентификатор продукта, к которому относится данное изображение
    public int? ProductId { get; set; }

    // Продукт, к которому относится данное изображение
    public ProductsEntity Products { get; set; }
}