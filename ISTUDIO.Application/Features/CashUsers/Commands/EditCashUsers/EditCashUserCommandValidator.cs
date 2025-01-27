
namespace ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;

public class EditCashUserCommandValidator : AbstractValidator<EditCashUserCommand>
{
    public EditCashUserCommandValidator()
    {
        // Проверка на непустое значение и диапазон для Id
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0.");

        // Проверка на непустой UserId
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым.")
            .MaximumLength(100).WithMessage("Идентификатор пользователя не должен превышать 100 символов.");

        // Проверка суммы (должна быть больше 0)
        RuleFor(v => v.Amount)
            .GreaterThan(0).WithMessage("Сумма должна быть больше 0.");

        // Проверка даты истечения
        RuleFor(v => v.ExpirationDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Дата истечения должна быть в будущем или сегодняшней.");

        // Проверка статуса
        RuleFor(v => v.Status)
            .NotEmpty().WithMessage("Статус не может быть пустым.")
            .Must(IsValidStatus).WithMessage("Указан недопустимый статус.");
    }
    private bool IsValidStatus(string status)
    {
        // Предположим, допустимые статусы: Active, Inactive, Expired
        var validStatuses = new[] { "Active", "Inactive", "Expired" };
        return validStatuses.Contains(status);
    }
}
