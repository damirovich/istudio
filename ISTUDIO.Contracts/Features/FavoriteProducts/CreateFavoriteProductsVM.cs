using ISTUDIO.Application.Features.FavoriteProducts.Commands;

namespace ISTUDIO.Contracts.Features.FavoriteProducts;

public class CreateFavoriteProductsVM : IMapWith<CreateFavoriteProductsCommand>
{
    public string UserId { get; set; }
    public int ProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFavoriteProductsVM, CreateFavoriteProductsCommand>();
    }
}
