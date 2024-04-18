using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Contracts.Features.Categories;

public class CreateCategoriesVM: IMapWith<CreateCategoriesCommand>
{
    [Required(ErrorMessage = "Name Category is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string PhotoCategoryBase64 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoriesVM, CreateCategoriesCommand>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.PhotoCategory, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoCategoryBase64)));


        profile.CreateMap<CategoryResponseDTO, CreateCategoriesVM>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            
    }

}
