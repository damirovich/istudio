using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Contracts.Features.Categories;

/// <summary>
/// Модель запроса для создания категории
/// </summary>
public class CreateCategoriesVM : IMapWith<CreateCategoriesCommand>
{
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
    /// Настройка маппинга между CreateCategoriesVM и CreateCategoriesCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoriesVM, CreateCategoriesCommand>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.PhotoCategory, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoCategoryBase64)))
             .ForMember(dest => dest.IconPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.IcontPhotoCategoryBase64)));

        profile.CreateMap<CategoryResponseDTO, CreateCategoriesVM>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}