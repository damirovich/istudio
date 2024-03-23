

using ISTUDIO.Application.Features.Categories.Commands.EditCategories;

namespace ISTUDIO.Contracts.Features.Categories;

public class EditCategoriesVM : IMapWith<EditCategoriesCommand>
{
    [Required(ErrorMessage = "Id  is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name Category is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCategoriesVM, EditCategoriesCommand>();
    }
}
