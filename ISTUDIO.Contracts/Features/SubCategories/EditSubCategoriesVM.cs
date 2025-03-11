using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

namespace ISTUDIO.Contracts.Features.SubCategories;

/// <summary>
/// Модель для редактирования подкатегории.
/// </summary>
public class EditSubCategoriesVM : IMapWith<EditSubCategoriesCommand>
{
    /// <summary>
    /// Уникальный идентификатор подкатегории.
    /// </summary>
    [Required(ErrorMessage = "Id SubCategory is required.")]
    public int Id { get; set; }

    /// <summary>
    /// Название подкатегории.
    /// </summary>
    [Required(ErrorMessage = "Name SubCategory is required.")]
    public string Name { get; set; }

    /// <summary>
    /// Описание подкатегории (необязательно).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Фото подкатегории в формате Base64 (необязательно).
    /// </summary>
    public string? PhotoCategoryBase64 { get; set; }

    /// <summary>
    /// Иконка подкатегории в формате Base64 (необязательно).
    /// </summary>
    public string? IcontPhotoCategoryBase64 { get; set; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    [Required(ErrorMessage = "CategoryId is required.")]
    public int CategoryId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditSubCategoriesVM и EditSubCategoriesCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditSubCategoriesVM, EditSubCategoriesCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PhotoCategory, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.PhotoCategoryBase64) ? null : Convert.FromBase64String(src.PhotoCategoryBase64)))
            .ForMember(dest => dest.IconPhotoCategory, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.IcontPhotoCategoryBase64) ? null : Convert.FromBase64String(src.IcontPhotoCategoryBase64)))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
    }
}