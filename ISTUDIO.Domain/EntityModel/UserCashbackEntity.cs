namespace ISTUDIO.Domain.EntityModel;

public class UserCashbackEntity
{
    public int Id { get; set; } // Уникальный идентификатор кешбэка
    public string UserId { get; set; } // Связь с пользователем
    public decimal Amount { get; set; } // Сумма кешбэка
    public DateTime CreatedAt { get; set; } // Дата создания кешбэка
    public DateTime ExpirationDate { get; set; } // Срок действия кешбэка
    public string Status { get; set; } // Статус кешбэка: Active, Used, Expired
}

