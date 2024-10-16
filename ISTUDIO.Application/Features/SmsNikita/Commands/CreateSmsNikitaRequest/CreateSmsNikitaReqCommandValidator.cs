namespace ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;

public class CreateSmsNikitaReqCommandValidator : AbstractValidator<CreateSmsNikitaReqCommand>
{
    public CreateSmsNikitaReqCommandValidator()
    {
        RuleFor(x => x.PhonesNumber)
           .NotEmpty().WithMessage("Список номеров телефонов не должен быть пустым.")
           .Must(p => p != null && p.Count > 0).WithMessage("Должен быть указан хотя бы один номер телефона.")
           .ForEach(phone =>
           {
               phone.NotEmpty().WithMessage("Номер телефона не должен быть пустым.")
                    .Must(BeValidPhoneNumber).WithMessage("Номер телефона должен быть в правильном формате (от 11 до 13 цифр, может начинаться с '+').");
           });

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Сообщение не должно быть пустым.")
            .MaximumLength(1000).WithMessage("Сообщение не должно превышать 1000 символов.");
    }
    private bool BeValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Проверяем, что номер соответствует одному из двух форматов
        if (phoneNumber.StartsWith("996") && phoneNumber.Length == 12)
        {
            // Проверяем, что все символы после "996" - цифры
            return phoneNumber.Skip(3).All(char.IsDigit);
        }
        else if (phoneNumber.StartsWith("+996") && phoneNumber.Length == 13)
        {
            // Проверяем, что все символы после "+996" - цифры
            return phoneNumber.Skip(4).All(char.IsDigit);
        }

        // Если номер не соответствует ни одному из двух форматов
        return false;
    }
}
