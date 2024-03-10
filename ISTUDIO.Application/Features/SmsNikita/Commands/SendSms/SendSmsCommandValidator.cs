namespace ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;

public class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
    public SendSmsCommandValidator()
    {
        RuleFor(command => command.PhonesNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone number is required.")
            .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format.");

    }
    private bool BeValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Проверяем, что номер начинается с '+' или цифры
        if (phoneNumber[0] != '+' && !char.IsDigit(phoneNumber[0]))
            return false;

        // Если номер начинается с '+', то длина должна быть не менее 13 символов
        if (phoneNumber[0] == '+' && phoneNumber.Length < 13)
            return false;

        // Если номер начинается с цифры, то длина должна быть не менее 11 символов
        if (char.IsDigit(phoneNumber[0]) && phoneNumber.Length < 12)
            return false;

        // Проверяем, что остальные символы - только цифры
        for (int i = 1; i < phoneNumber.Length; i++)
        {
            if (!char.IsDigit(phoneNumber[i]))
                return false;
        }

        return true;
    }
}
