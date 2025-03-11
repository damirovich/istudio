namespace ISTUDIO.Contracts.Features.OrderPayments;
/// <summary>
/// Модель для создания оплаты заказа.
/// </summary>
public class CreateOrderPaymentVM
{
    /// <summary>
    /// Идентификатор пользователя, который совершил оплату.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Уникальный номер заказа.
    /// </summary>
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrderId { get; set; }

    /// <summary>
    /// ID метода оплаты:
    /// 2 - Bakai, 3 - mBank, 4 - Optima Bank, 5 - Cash, 6 - FreedomPay.
    /// </summary>
    [Required(ErrorMessage = "PaymentMethodId is required.")]
    public int PaymentMethodId { get; set; }

    /// <summary>
    /// Сумма заказа до применения бонусов.
    /// </summary>
    public decimal? InitialAmount { get; set; }

    /// <summary>
    /// Итоговая сумма после вычета бонусов.
    /// </summary>
    [Required(ErrorMessage = "FinalAmount is required.")]
    public decimal FinalAmount { get; set; }

    /// <summary>
    /// Примененная сумма бонусов.
    /// </summary>
    public decimal? BonusAmount { get; set; }

    /// <summary>
    /// Начисленная сумма бонусов.
    /// </summary>
    public decimal? EarnedBonusAmount { get; set; }

    /// <summary>
    /// Номер телефона, используемый при оплате через Bakai Bank (необязательно).
    /// </summary>
    public string? BakaiPhoneNumber { get; set; }
}