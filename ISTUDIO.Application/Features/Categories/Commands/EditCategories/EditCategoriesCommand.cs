
namespace ISTUDIO.Application.Features.Categories.Commands.EditCategories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditCategoriesCommand : IRequest<ResModel>, IMapWith<CategoryEntity>
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[] PhotoCategory { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCategoriesCommand, CategoryEntity>();
    }
}
