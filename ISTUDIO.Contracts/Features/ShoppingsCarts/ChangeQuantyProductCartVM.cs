using ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;

namespace ISTUDIO.Contracts.Features.ShoppingsCarts;

/// <summary>
/// Модель для изменения количества товара в корзине.
/// </summary>
public class ChangeQuantyProductCartVM : IMapWith<ChangeQuantyProductCartCommand>
{
    /// <summary>
    /// Уникальный идентификатор корзины.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Новое количество продукта в корзине.
    /// </summary>
    public int QuantyProduct { get; set; }

    /// <summary>
    /// Конфигурация маппинга между ChangeQuantyProductCartVM и ChangeQuantyProductCartCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChangeQuantyProductCartVM, ChangeQuantyProductCartCommand>();
    }
}