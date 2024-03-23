using ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

namespace ISTUDIO.Contracts.Features.SubCategories;

public class CreateSubCategoriesVM : IMapWith<CreateSubCategoriesCommand>
{ 
    [Required(ErrorMessage = "Name SubCategory is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public int CategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSubCategoriesVM, CreateSubCategoriesCommand>();
    }
}
