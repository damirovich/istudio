using ISTUDIO.Application.Features.Products.Commands.UpdateProductIsActive;

namespace ISTUDIO.Contracts.Features.Products;

public class UpdateProductActiveVM : IMapWith<UpdateProductActiveCommand>
{
    public int ProductId { get; set; }
    public bool ProductActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductActiveVM, UpdateProductActiveCommand>();
    }
}
