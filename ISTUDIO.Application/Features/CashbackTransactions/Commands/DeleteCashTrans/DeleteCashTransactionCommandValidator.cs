namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.DeleteCashTrans;

public class DeleteCashTransactionCommandValidator : AbstractValidator<DeleteCashTransactionCommand>
{
    public DeleteCashTransactionCommandValidator()
    {
        RuleFor(v => v.CashTranId).NotEmpty().WithMessage("CashTranId не должен быть пустым.")
            .GreaterThan(0).WithMessage("CashTranId должен быть положительным числом.");
    }
}
