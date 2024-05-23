

using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Contracts.Features.Categories;

public class EditCategoriesVM : IMapWith<EditCategoriesCommand>
{
    [Required(ErrorMessage = "Id  is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name Category is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string PhotoCategoryBase64 { get; set; }
    public string IcontPhotoCategoryBase64 { get; set; }
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
