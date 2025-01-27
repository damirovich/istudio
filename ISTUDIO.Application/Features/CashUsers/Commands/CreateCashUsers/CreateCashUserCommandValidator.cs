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


        RuleFor(v => v.Status)
            .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов.");
    }
}
