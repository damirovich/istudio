namespace ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditSubCategoriesCommand : IRequest<ResModel>, IMapWith<CategoryEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditSubCategoriesCommand, CategoryEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.CategoryId));
    }
}
