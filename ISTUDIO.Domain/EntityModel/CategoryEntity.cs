namespace ISTUDIO.Domain.EntityModel;

public class CategoryEntity
{
    // Уникальный идентификатор категории
    public int Id { get; set; }

    // Название категории
    public string Name { get; set; }

    // Описание категории
    public string Description { get; set; }

    // URL изображения, связанного с категорией
    public string ImageUrl { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryEntity ParentCategory { get; set; }

    // Коллекция подкатегорий, связанных с данной категорией
    public ICollection<CategoryEntity> SubCategories { get; set; }= new List<CategoryEntity>();
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}