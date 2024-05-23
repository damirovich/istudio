using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Categories.DTOs;

public class CategoriesListResponseDTO : IMapWith<CategoryEntity>
{
    public List<CategoryDTO> Categories { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<List<CategoryEntity>, List<CategoryDTO>>()
         .ConvertUsing(src => MapCategories(src));
    }

    // Метод MapCategories принимает список категорий типа CategoryEntity и преобразует их в список DTO категорий типа CategoryDTO.
    private List<CategoryDTO> MapCategories(List<CategoryEntity> categories)
    {
        // Создаем новый список для хранения DTO категорий.
        var categoryDTOs = new List<CategoryDTO>();

        // Создаем хэш-сет для хранения идентификаторов всех подкатегорий.
        var subCategoryIds = new HashSet<int>();

        // Добавляем идентификаторы всех подкатегорий в хэш-сет.
        foreach (var category in categories)
        {
            foreach (var subCategory in category.SubCategories)
            {
                subCategoryIds.Add(subCategory.Id);
            }
        }

        // Добавляем только категории, которые не являются подкатегориями.
        foreach (var category in categories)
        {
            // Проверяем, является ли текущая категория подкатегорией какой-либо другой категории.
            // Если нет, то добавляем ее в список DTO категорий.
            if (!subCategoryIds.Contains(category.Id))
            {
                // Создаем новый объект DTO для текущей категории.
                var categoryDTO = new CategoryDTO
                {
                    Id = category.Id,
                    // Устанавливаем название категории из свойства Name категории.
                    Name = category.Name,
                    // Устанавливаем URL изображения категории из свойства ImageUrl категории.
                    PhotoURL = category.ImageUrl,
                    // Устанавливаем URL Икон изображение категории из свойства IconImageUrl категории.
                    IconPhotoURL = category.IconImageUrl,
                    // Вызываем метод MapSubCategories для преобразования подкатегорий текущей категории.
                    SubCategories = MapSubCategories(category.SubCategories)
                };

                // Добавляем DTO текущей категории в список DTO категорий.
                categoryDTOs.Add(categoryDTO);
            }
        }

        // Возвращаем список DTO категорий.
        return categoryDTOs;
    }

    // Метод MapSubCategories принимает коллекцию подкатегорий типа CategoryEntity и преобразует их в список DTO подкатегорий типа CategoryDTO.
    private List<CategoryDTO> MapSubCategories(ICollection<CategoryEntity> subCategories)
    {
        // Создаем новый список для хранения DTO подкатегорий.
        var subCategoryDTOs = new List<CategoryDTO>();

        // Проходимся по каждой подкатегории в коллекции.
        foreach (var subCategory in subCategories)
        {
            // Создаем новый объект DTO для текущей подкатегории.
            var subCategoryDTO = new CategoryDTO
            {
                Id = subCategory.Id,
                // Устанавливаем название подкатегории из свойства Name подкатегории.
                Name = subCategory.Name,
                // Устанавливаем URL изображения подкатегории из свойства ImageUrl подкатегории.
                PhotoURL = subCategory.ImageUrl,

                // Устанавливаем URL изображения подкатегории из свойства IconImageUrl подкатегории.
                IconPhotoURL = subCategory.IconImageUrl,

                // Рекурсивно вызываем этот же метод для преобразования подкатегорий текущей подкатегории, если они есть.
                SubCategories = MapSubCategories(subCategory.SubCategories)
            };

            // Добавляем DTO текущей подкатегории в список DTO подкатегорий.
            subCategoryDTOs.Add(subCategoryDTO);
        }

        // Возвращаем список DTO подкатегорий.
        return subCategoryDTOs;
    }
}


