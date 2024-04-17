using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Categories.DTOs;

public class CategoryDTO : IMapWith<CategoryEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhotoURL { get; set; }
    public List<CategoryDTO>? SubCategories { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryEntity, CategoryDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
    }
}
