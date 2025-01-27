namespace ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;

public class CreateCashbackCommandValidator : AbstractValidator<CreateCashbackCommand>
{
    public CreateCashbackCommandValidator()
    {
        RuleFor(x => x.CashbackPercent)
            .InclusiveBetween(0, 100)
            .WithMessage("Процент кешбэка должен быть в диапазоне от 0 до 100.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Дата начала не может быть пустой.")
            .LessThan(x => x.EndDate)
            .WithMessage("Дата начала должна быть раньше даты окончания.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("Дата окончания не может быть пустой.")
            .GreaterThan(DateTime.Now)
            .WithMessage("Дата окончания должна быть позже текущей даты.");

        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("Поле IsActive не может быть null.");
    }
}