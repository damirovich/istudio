using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;

namespace ISTUDIO.Contracts.Features.Categories;

public class CreateCategoriesVM: IMapWith<CreateCategoriesCommand>
{
    [Required(ErrorMessage = "Name Category is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoriesVM, CreateCategoriesCommand>();
    }

}
