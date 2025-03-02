using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Contracts.Features.Categories;

/// <summary>
/// Модель запроса для редактирования категории
/// </summary>
public class EditCategoriesVM : IMapWith<EditCategoriesCommand>
{
    /// <summary>
    /// Идентификатор категории (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Идентификатор категории обязателен.")]
    public int Id { get; set; }

    /// <summary>
    /// Название категории (обязательно)
    /// </summary>
    [Required(ErrorMessage = "Название категории обязательно.")]
    public string Name { get; set; }

    /// <summary>
    /// Описание категории (необязательно)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Фото категории в формате Base64
    /// </summary>
    public string PhotoCategoryBase64 { get; set; }

    /// <summary>
    /// Иконка категории в формате Base64
    /// </summary>
    public string IcontPhotoCategoryBase64 { get; set; }

    /// <summary>
    /// Настройка маппинга между EditCategoriesVM и EditCategoriesCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCategoriesVM, EditCategoriesCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PhotoCategory, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoCategoryBase64)))
            .ForMember(dest => dest.IconPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.IcontPhotoCategoryBase64)));

        profile.CreateMap<CategoryResponseDTO, EditCategoriesVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PhotoCategoryBase64, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.IcontPhotoCategoryBase64, opt => opt.MapFrom(src => src.IconImageUrl));
    }
}
