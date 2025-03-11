namespace ISTUDIO.Contracts.Features.BakaiPay;

/// <summary>
/// Модель для создания платежа через BakaiPay.
/// </summary>
public class BakaiCreatePayVM
{
    /// <summary>
    /// Номер телефона пользователя, который совершает платеж.
    /// </summary>
    [Required]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Общая сумма товаров в заказе (в сомах).
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
    public int SumProducts { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    [Required]
    public int OrderId { get; set; }
}