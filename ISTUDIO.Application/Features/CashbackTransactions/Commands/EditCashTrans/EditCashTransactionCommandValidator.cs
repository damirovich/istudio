namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;

public class EditCashTransactionCommandValidator : AbstractValidator<EditCashTransactionCommand>
{
    public EditCashTransactionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id должен быть больше 0.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId не может быть пустым.")
            .Length(3, 50)
            .WithMessage("UserId должен содержать от 3 до 50 символов.");

        RuleFor(x => x.OrderId)
            .GreaterThan(0).When(x => x.OrderId.HasValue)
            .WithMessage("OrderId, если указан, должен быть больше 0.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Сумма транзакции должна быть больше 0.");

        RuleFor(x => x.TransactionType)
            .NotEmpty()
            .WithMessage("Тип транзакции не может быть пустым.")
            .Must(type => type == "Credit" || type == "Debit")
            .WithMessage("Тип транзакции должен быть либо 'Credit', либо 'Debit'.");
    }
}
