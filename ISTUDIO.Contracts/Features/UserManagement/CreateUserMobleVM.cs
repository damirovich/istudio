namespace ISTUDIO.Contracts.Features.UserManagement;
/// <summary>
/// Модель запроса для регистрации пользователя в мобильном приложении
/// </summary>
public class CreateUserMobleVM
{
    /// <summary>
    /// Номер телефона пользователя
    /// </summary>
    [Required(ErrorMessage = "Телефонный номер обязателен.")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Одноразовый пароль (OTP) для подтверждения номера
    /// </summary>
    [Required(ErrorMessage = "Одноразовый пароль обязателен.")]
    public int CodeOTP { get; set; }

    /// <summary>
    /// Флаг согласия с политикой конфиденциальности
    /// </summary>
    [Required(ErrorMessage = "Согласие с политикой конфиденциальности обязательно.")]
    public bool HasAgreedToPrivacyPolicy { get; set; }

    /// <summary>
    /// Флаг согласия с пользовательским соглашением
    /// </summary>
    [Required(ErrorMessage = "Согласие с пользовательским соглашением обязательно.")]
    public bool ConsentToTheUserAgreement { get; set; }
}
