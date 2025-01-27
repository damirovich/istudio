namespace ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;

public class EditCashbackCommandValidator : AbstractValidator<EditCashbackCommand>
{
    public EditCashbackCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id должен быть больше 0.");

        RuleFor(x => x.CashbackPercent)
            .InclusiveBetween(0, 100)
            .WithMessage("Процент кешбэка должен быть в диапазоне от 0 до 100.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Дата начала должна быть раньше даты окончания.");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Дата окончания должна быть позже текущей даты.");

        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("Поле IsActive не может быть null.");
    }
}
