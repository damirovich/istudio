
namespace ISTUDIO.Contracts.Features.ShoppingsCarts;

/// <summary>
/// Модель для проверки наличия продукта в корзине пользователя.
/// </summary>
public class CheckProductsCartsVM
{
    /// <summary>   
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string UserId { get; set; }
}
