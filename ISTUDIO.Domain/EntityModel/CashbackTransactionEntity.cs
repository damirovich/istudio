namespace ISTUDIO.Domain.EntityModel;

public class CashbackTransactionEntity
{
    public int Id { get; set; } // Уникальный идентификатор транзакции
    public string UserId { get; set; } // Связь с пользователем
    public int? OrderId { get; set; } // Связь с заказом (если есть)
    public decimal Amount { get; set; } // Сумма транзакции
    public string TransactionType { get; set; } // Тип транзакции: "Credit", "Debit"
    public DateTime CreatedAt { get; set; } // Дата транзакции

    // Навигационные свойства
    public  OrderEntity Order { get; set; } // Связь с заказом (опционально)

}
