using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using ISTUDIO.Application.Features.Products.Commands.EditProducts;

namespace ISTUDIO.Contracts.Features.Products;

public class EditProductsVM : IMapWith<EditProductsCommand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditProductsVM, EditProductsCommand>();
    }
}
