namespace ISTUDIO.Domain.EntityModel;

public class ProductsEntity
{
    // Уникальный идентификатор продукта
    public int Id { get; set; }

    // Название продукта
    public string Name { get; set; }

    // Модель продукта
    public string Model { get; set; }

    // Цвет продукта
    public string Color { get; set; }

    // Цена продукта
    public decimal Price { get; set; }

    // Количество товара в наличии
    public int QuantityInStock { get; set; }

    // Описание продукта
    public string Description { get; set; }

    // Идентификатор категории, к которой относится продукт
    public int? CategoryId { get; set; }

    // Идентификатор подкатегории, к которой относится продукт
    public int? SubCategoryId { get; set; }

    // Категория, к которой относится продукт
    public CategoryEntity Category { get; set; }

    // Подкатегория, к которой относится продукт
    public SubCategoryEntity SubCategory { get; set; }
    // Скидка на продукт
    public DiscountEntity Discount { get; set; }

    // Коллекция изображений продукта
    public ICollection<ProducImagesEntity> Images { get; set; } = new List<ProducImagesEntity>();
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
