namespace ISTUDIO.Application.Features.SmsNikita.Commands.BulkSms;

public class SendBulkSmsCommandValidator : AbstractValidator<SendBulkSmsCommand>
{
    public SendBulkSmsCommandValidator()
    {
        RuleFor(x => x.PhonesNumber)
            .NotEmpty().WithMessage("Список номеров телефонов не должен быть пустым.")
            .Must(p => p.Count > 0).WithMessage("Должен быть указан хотя бы один номер телефона.")
            .ForEach(phone =>
            {
                phone.NotEmpty().WithMessage("Номер телефона не должен быть пустым.")
                     .Matches(@"^\+?\d+$").WithMessage("Номер телефона должен быть в правильном формате.");
            });

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Сообщение не должно быть пустым.")
            .MaximumLength(350).WithMessage("Сообщение не должно превышать 350 символов.");
    }
}
