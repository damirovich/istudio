namespace ISTUDIO.Domain.EntityModel;

public class SubCategoryEntity
{
    // Уникальный идентификатор подкатегории
    public int Id { get; set; }

    // Название подкатегории
    public string Name { get; set; }

    // Описание подкатегории
    public string Description { get; set; }

    // URL изображения, связанного с подкатегорией
    public string ImageUrl { get; set; }

    // Идентификатор категории, к которой относится данная подкатегория
    public int? CategoryId { get; set; }

    // Ссылка на объект категории, к которой относится данная подкатегория
    public CategoryEntity Category { get; set; }
}