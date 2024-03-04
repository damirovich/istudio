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

    // Коллекция подкатегорий, связанных с данной категорией
    public ICollection<SubCategoryEntity> SubCategories { get; set; }= new List<SubCategoryEntity>();
    public ICollection<ProductsEntity> Products { get; set; } = new List<ProductsEntity>();
}