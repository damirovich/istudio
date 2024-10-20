
using ISTUDIO.Application.Features.Products.Commands.UpdateProductSum;

namespace ISTUDIO.Contracts.Features.Products;

public class UpdateProductSummVM : IMapWith<UpdateProductSummaCommand>
{
    public int ProductId { get; set; }
    public decimal ProductSumma { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductSummVM, UpdateProductSummaCommand>();
    }
}
