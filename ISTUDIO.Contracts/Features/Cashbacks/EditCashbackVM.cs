using ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;

namespace ISTUDIO.Contracts.Features.Cashbacks;

/// <summary>
/// Модель для редактирования кэшбэка.
/// </summary>
public class EditCashbackVM : IMapWith<EditCashbackCommand>
{
    /// <summary>
    /// Уникальный идентификатор кэшбэка.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Процент кэшбэка (от 0 до 100).
    /// </summary>
    public decimal CashbackPercent { get; set; }

    /// <summary>
    /// Дата начала действия кэшбэка.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания действия кэшбэка.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Флаг активности кэшбэка.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditCashbackVM и EditCashbackCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashbackVM, EditCashbackCommand>();
    }
}