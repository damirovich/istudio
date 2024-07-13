namespace ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;

public class RegistrUsersMobileCommandValidator : AbstractValidator<RegistrUsersMobileCommand>
{
    public RegistrUsersMobileCommandValidator()
    {
        RuleFor(v => v.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone number is required.")
            .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format.");

        RuleFor(v => v.OTPCode)
            .NotEmpty().WithMessage("OTP code is required.")
            .Must(BeValidOTPCode).WithMessage("OTP code must be a 6-digit number.");

        RuleFor(v => v.HasAgreedToPrivacyPolicy)
            .NotEmpty().WithMessage("HasAgreedToPrivacyPolicy code is required.");

        RuleFor(v => v.ConsentToTheUserAgreement)
             .NotEmpty().WithMessage("ConsentToTheUserAgreement code is required.");

    }
    private bool BeValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Проверяем, что номер начинается с '+' или цифры
        if (phoneNumber[0] != '+' && !char.IsDigit(phoneNumber[0]))
            return false;

        // Если номер начинается с '+', то длина должна быть не менее 12 символов
        if (phoneNumber[0] == '+' && phoneNumber.Length < 12)
            return false;

        // Если номер начинается с цифры, то длина должна быть не менее 11 символов
        if (char.IsDigit(phoneNumber[0]) && phoneNumber.Length < 11)
            return false;

        // Проверяем, что остальные символы - только цифры
        for (int i = 1; i < phoneNumber.Length; i++)
        {
            if (!char.IsDigit(phoneNumber[i]))
                return false;
        }

        return true;
    }
    private bool BeValidOTPCode(int otpCode)
    {
        // Проверяем, что otpCode - 6-значное число
        return otpCode >= 100000 && otpCode <= 999999;
    }
}
