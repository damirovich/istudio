using ISTUDIO.Application.Features.CashbackProduct.Commands;

namespace ISTUDIO.Contracts.Features.CashbackProducts;

/// <summary>
/// Модель запроса для создания кэшбэк-продукта
/// </summary>
public class CreateCashbackProductVM : IMapWith<CreateCashbackProductCommand>
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Максимальный процент кэшбэка
    /// </summary>
    public decimal MaxBonusPercent { get; set; }

    /// <summary>
    /// Настройка маппинга между CreateCashbackProductVM и CreateCashbackProductCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashbackProductVM, CreateCashbackProductCommand>();
    }
}