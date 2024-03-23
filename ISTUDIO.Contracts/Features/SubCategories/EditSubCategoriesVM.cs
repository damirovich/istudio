using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

namespace ISTUDIO.Contracts.Features.SubCategories;

public class EditSubCategoriesVM : IMapWith<EditSubCategoriesCommand>
{
    [Required(ErrorMessage = "Id SubCategory is required.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name SubCategory is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public int CategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditSubCategoriesVM, EditSubCategoriesCommand>();
    }
}
