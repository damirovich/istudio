namespace ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;

public class EditDiscountsCommandValidator : AbstractValidator<EditDiscountsCommand>
{
    public EditDiscountsCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id обязателен для заполнения.")
            .GreaterThan(0).WithMessage("Id должен быть положительным числом.");

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
        // Customize this method according to your specific DateTime validation logic
        return dateTime != default;
    }
}
