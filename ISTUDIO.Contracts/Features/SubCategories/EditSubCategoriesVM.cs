using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

namespace ISTUDIO.Contracts.Features.SubCategories;

public class EditSubCategoriesVM : IMapWith<EditSubCategoriesCommand>
{
    [Required(ErrorMessage = "Id SubCategory is required.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name SubCategory is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string PhotoCategoryBase64 { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public int CategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditSubCategoriesVM, EditSubCategoriesCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PhotoCategory, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoCategoryBase64)))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
    }
}
