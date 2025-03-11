using ISTUDIO.Application.Features.FavoriteProducts.Commands.CreateFavoriteProducts;

namespace ISTUDIO.Contracts.Features.FavoriteProducts;

/// <summary>
/// Модель для добавления товара в избранное.
/// </summary>
public class CreateFavoriteProductsVM : IMapWith<CreateFavoriteProductsCommand>
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Идентификатор продукта, который добавляется в избранное.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateFavoriteProductsVM и CreateFavoriteProductsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFavoriteProductsVM, CreateFavoriteProductsCommand>();
    }
}