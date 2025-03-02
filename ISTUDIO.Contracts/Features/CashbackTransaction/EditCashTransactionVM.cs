using ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;

namespace ISTUDIO.Contracts.Features.CashbackTransaction;

/// <summary>
/// Модель запроса для редактирования кэшбэк-транзакции
/// </summary>
public class EditCashTransactionVM : IMapWith<EditCashTransactionCommand>
{
    /// <summary>
    /// Идентификатор кэшбэк-транзакции
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя, связанного с транзакцией
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Идентификатор заказа (необязательно)
    /// </summary>
    public int? OrderId { get; set; }

    /// <summary>
    /// Сумма транзакции
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Тип транзакции (например, начисление или списание)
    /// </summary>
    public string TransactionType { get; set; }

    /// <summary>
    /// Настройка маппинга между EditCashTransactionVM и EditCashTransactionCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashTransactionVM, EditCashTransactionCommand>();
    }
}