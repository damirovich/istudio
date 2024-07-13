namespace ISTUDIO.Contracts.Features.UserManagement;

public class CreateUserMobleVM
{
    [Required(ErrorMessage = "Телефонный номер обязателен.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Одноразовый пароль обязателен.")]
    public int CodeOTP { get; set; }

    [Required(ErrorMessage = "Согласие с политикой конфиденциальности обязательно.")]
    public bool HasAgreedToPrivacyPolicy { get; set; }

    [Required(ErrorMessage ="Согласие с пользовательским соглашением обязательно.")]
    public bool ConsentToTheUserAgreement { get; set; }

}
