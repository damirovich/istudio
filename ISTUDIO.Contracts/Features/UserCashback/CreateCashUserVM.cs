using ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;

namespace ISTUDIO.Contracts.Features.UserCashback;

/// <summary>
/// Модель для создания записи о денежных средствах пользователя.
/// </summary>
public class CreateCashUserVM : IMapWith<CreateCashUserCommand>
{
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
    /// Конфигурация маппинга между CreateCashUserVM и CreateCashUserCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashUserVM, CreateCashUserCommand>();
    }
}