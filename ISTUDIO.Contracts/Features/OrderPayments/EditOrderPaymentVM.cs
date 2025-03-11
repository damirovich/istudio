using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;

namespace ISTUDIO.Contracts.Features.OrderPayments;

/// <summary>
/// Модель для редактирования данных об оплате заказа.
/// </summary>
public class EditOrderPaymentVM : IMapWith<EditOrderPaymentCommands>
{
    /// <summary>
    /// Уникальный идентификатор платежа.
    /// </summary>
    [Required(ErrorMessage = "Payment ID is required.")]
    public int Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrderId { get; set; }

    /// <summary>
    /// Идентификатор метода оплаты.
    /// 2 - Bakai, 3 - mBank, 4 - Optima Bank, 5 - Cash, 6 - FreedomPay.
    /// </summary>
    [Required(ErrorMessage = "PaymentMethodId is required.")]
    public int PaymentMethodId { get; set; }

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [Required(ErrorMessage = "Amount is required.")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Статус платежа (например, "Pending", "Completed", "Failed").
    /// </summary>
    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; }

    /// <summary>
    /// Уникальный идентификатор транзакции (если есть).
    /// </summary>
    public string? TransactionId { get; set; }

    /// <summary>
    /// Фото чека в формате Base64 (если есть).
    /// </summary>
    public string? ReceiptPhoto { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditOrderPaymentVM и EditOrderPaymentCommands.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditOrderPaymentVM, EditOrderPaymentCommands>();
    }
}