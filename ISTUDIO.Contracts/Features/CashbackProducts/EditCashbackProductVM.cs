using ISTUDIO.Application.Features.CashbackProduct.Commands;

namespace ISTUDIO.Contracts.Features.CashbackProducts;

/// <summary>
/// Модель запроса для редактирования кэшбэк-продукта
/// </summary>
public class EditCashbackProductVM : IMapWith<EditCashbackProductCommand>
{
    /// <summary>
    /// Идентификатор кэшбэк-продукта
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор связанного продукта
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Максимальный процент кэшбэка
    /// </summary>
    public decimal MaxBonusPercent { get; set; }

    /// <summary>
    /// Настройка маппинга между EditCashbackProductVM и EditCashbackProductCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashbackProductVM, EditCashbackProductCommand>();
    }
}