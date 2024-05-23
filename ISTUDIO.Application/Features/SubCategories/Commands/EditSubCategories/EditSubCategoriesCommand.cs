namespace ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditSubCategoriesCommand : IRequest<ResModel>, IMapWith<CategoryEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[]? PhotoCategory { get; set; }
    public byte[]? IconPhotoCategory { get; set; }
    public int CategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditSubCategoriesCommand, CategoryEntity>();
    }
}
