using ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;

namespace ISTUDIO.Contracts.Features.Products;

public class UpdateProductQuantityVM : IMapWith<UpdateProductQuantityCommand>
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductQuantityVM, UpdateProductQuantityCommand>();
    }
}
