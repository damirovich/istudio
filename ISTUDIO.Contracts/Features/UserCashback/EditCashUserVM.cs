using ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;

namespace ISTUDIO.Contracts.Features.UserCashback;

/// <summary>
/// Модель для редактирования записи о денежных средствах пользователя.
/// </summary>
public class EditCashUserVM : IMapWith<EditCashUserCommand>
{
    /// <summary>
    /// Уникальный идентификатор записи денежных средств.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Сумма денежных средств.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Дата истечения срока действия денежных средств.
    /// </summary>
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Статус денежных средств (например, "Active", "Expired").
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditCashUserVM и EditCashUserCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashUserVM, EditCashUserCommand>();
    }
}