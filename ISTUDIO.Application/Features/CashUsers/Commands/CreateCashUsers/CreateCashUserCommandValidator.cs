namespace ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;

public class CreateCashUserCommandValidator : AbstractValidator<CreateCashUserCommand>
{
    public CreateCashUserCommandValidator()
    {
        RuleFor(v => v.UserId)
         .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым.");

        RuleFor(v => v.Amount)
            .GreaterThan(0).WithMessage("Сумма кэшбэка должна быть больше нуля.");

        RuleFor(v => v.ExpirationDate)
           .NotEmpty().WithMessage("Дата не может быть пустой.") 
           .Must(date => date != default(DateTime)).WithMessage("Дата должна быть корректной.");

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
