using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Categories.DTOs;

public class CategoryResponseDTO : IMapWith<CategoryEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryEntity, CategoryResponseDTO>()
            .ReverseMap();

    }
}
