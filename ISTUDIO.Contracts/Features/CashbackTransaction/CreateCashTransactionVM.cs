
using ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;

namespace ISTUDIO.Contracts.Features.CashbackTransaction;

/// <summary>
/// Модель запроса для создания кэшбэк-транзакции
/// </summary>
public class CreateCashTransactionVM : IMapWith<CreateCashTransactionCommand>
{
    /// <summary>
    /// Идентификатор пользователя, совершающего транзакцию
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
    /// Настройка маппинга между CreateCashTransactionVM и CreateCashTransactionCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashTransactionVM, CreateCashTransactionCommand>();
    }
}