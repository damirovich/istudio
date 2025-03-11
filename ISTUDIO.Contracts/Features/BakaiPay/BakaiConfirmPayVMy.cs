namespace ISTUDIO.Contracts.Features.BakaiPay;

/// <summary>
/// Модель для подтверждения платежа через BakaiPay.
/// </summary>
public class BakaiConfirmPayVMy
{
    /// <summary>
    /// Уникальный идентификатор платежа.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Одноразовый пароль (OTP) для подтверждения платежа.
    /// </summary>
    [Required]
    [StringLength(6, MinimumLength = 4, ErrorMessage = "OTP должен содержать от 4 до 6 символов.")]
    public string OTP { get; set; }
}