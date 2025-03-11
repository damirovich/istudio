using ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;

namespace ISTUDIO.Contracts.Features.Cashbacks;

/// <summary>
/// Модель для создания кэшбэка.
/// </summary>
public class CreateCashbackVM : IMapWith<CreateCashbackCommand>
{
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
    /// Конфигурация маппинга между CreateCashbackVM и CreateCashbackCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashbackVM, CreateCashbackCommand>();
    }
}