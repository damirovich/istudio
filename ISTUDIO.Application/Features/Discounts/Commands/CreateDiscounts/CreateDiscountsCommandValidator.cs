namespace ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

public class CreateDiscountsCommandValidator : AbstractValidator<CreateDiscountsCommand>
{
    public CreateDiscountsCommandValidator()
    {
        RuleFor(v => v.PercenTage)
            .NotEmpty().WithMessage("Процент обязателен для заполнения.")
            .GreaterThan(0).WithMessage("Процент должен быть больше 0.")
            .LessThanOrEqualTo(100).WithMessage("Процент должен быть меньше или равен 100.");

        RuleFor(v => v.StartTime)
            .NotEmpty().WithMessage("Время начала обязательно для заполнения.")
            .Must(BeAValidDateTime).WithMessage("Некорректное время начала.");

        RuleFor(v => v.EndTime)
            .NotEmpty().WithMessage("Время окончания обязательно для заполнения.")
            .Must(BeAValidDateTime).WithMessage("Некорректное время окончания.")
            .GreaterThan(v => v.StartTime).WithMessage("Время окончания должно быть позже времени начала.");
    }

    private bool BeAValidDateTime(DateTime dateTime)
    {
        // Настройте этот метод в соответствии с вашей логикой проверки DateTime
        return dateTime != default;
    }
}
