using ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

namespace ISTUDIO.Contracts.Features.Discounts;

/// <summary>
/// Модель для создания скидки.
/// </summary>
public class CreateDiscountVM : IMapWith<CreateDiscountsCommand>
{
    /// <summary>
    /// Процент скидки (от 0 до 100).
    /// </summary>
    public decimal PercenTage { get; set; }

    /// <summary>
    /// Дата и время начала действия скидки.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Дата и время окончания действия скидки.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateDiscountVM и CreateDiscountsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateDiscountVM, CreateDiscountsCommand>();
    }
}