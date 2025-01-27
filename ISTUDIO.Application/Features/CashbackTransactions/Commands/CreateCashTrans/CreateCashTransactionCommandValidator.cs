using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;

public class CreateCashTransactionCommandValidator : AbstractValidator<CashbackTransactionEntity>
{
    public CreateCashTransactionCommandValidator()
    {
        RuleFor(v => v.UserId)
             .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым.")
             .MaximumLength(100).WithMessage("Идентификатор пользователя не должен превышать 100 символов.");

        RuleFor(v => v.OrderId)
            .GreaterThan(0).When(v => v.OrderId.HasValue).WithMessage("Идентификатор заказа должен быть больше 0.");

        RuleFor(v => v.Amount)
            .GreaterThan(0).WithMessage("Сумма транзакции должна быть больше 0.");

        RuleFor(v => v.TransactionType)
            .NotEmpty().WithMessage("Тип транзакции не может быть пустым.")
            .Must(type => type == "Credit" || type == "Debit").WithMessage("Тип транзакции должен быть 'Credit' или 'Debit'.");

    }
}
