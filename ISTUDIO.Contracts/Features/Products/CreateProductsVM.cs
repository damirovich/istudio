using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using System.Text;


namespace ISTUDIO.Contracts.Features.Products;

public class CreateProductsVM : IMapWith<CreateProductsCommand>
{
    public string Name { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? MagazineId { get; set; }
    public List<string> ProductPhotos { get; set; } = new List<string>();
   
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductsVM, CreateProductsCommand>();
    }
}
