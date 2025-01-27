namespace ISTUDIO.Application.Features.CashUsers.Commands.DeleteCashUsers;

public class DeleteCashUserCommandValidor : AbstractValidator<DeleteCashUserCommand>
{
    public DeleteCashUserCommandValidor()
    {
        RuleFor(v => v.CashbackId).NotEmpty().WithMessage("CashbackId не должен быть пустым.")
           .GreaterThan(0).WithMessage("CashbackId должен быть положительным числом.");
    }
}
