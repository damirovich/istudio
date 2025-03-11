using ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;

namespace ISTUDIO.Contracts.Features.Discounts;

/// <summary>
/// Модель для редактирования скидки.
/// </summary>
public class EditDiscountVM : IMapWith<EditDiscountsCommand>
{
    /// <summary>
    /// Уникальный идентификатор скидки.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Процент скидки (от 0 до 100).
    /// </summary>
    public decimal Percentage { get; set; }

    /// <summary>
    /// Дата и время начала действия скидки.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Дата и время окончания действия скидки.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditDiscountVM и EditDiscountsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditDiscountVM, EditDiscountsCommand>();
    }
}