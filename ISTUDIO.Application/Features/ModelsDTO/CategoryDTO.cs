
namespace ISTUDIO.Application.Features.ModelsDTO;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhotoURL { get; set; }
    public List<CategoryDTO> SubCategories { get; set; }
}
