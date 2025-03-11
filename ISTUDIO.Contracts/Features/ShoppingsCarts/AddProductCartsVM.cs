using ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;

namespace ISTUDIO.Contracts.Features.ShoppingsCarts;

/// <summary>
/// Модель для добавления продукта в корзину.
/// </summary>
public class AddProductCartsVM : IMapWith<AddProductToCartsCommand>
{
    /// <summary>
    /// Идентификатор пользователя, добавляющего товар в корзину.
    /// </summary>
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; }

    /// <summary>
    /// Уникальный идентификатор продукта, добавляемого в корзину.
    /// </summary>
    [Required(ErrorMessage = "ProductId is required.")]
    public int ProductId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между AddProductCartsVM и AddProductToCartsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddProductCartsVM, AddProductToCartsCommand>();
    }
}