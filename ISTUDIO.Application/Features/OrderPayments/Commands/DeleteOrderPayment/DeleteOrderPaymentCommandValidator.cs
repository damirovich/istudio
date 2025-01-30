namespace ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;

public class DeleteOrderPaymentCommandValidator : AbstractValidator<DeleteOrderPaymentCommands>
{
    public DeleteOrderPaymentCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("OrderPayId не должен быть пустым.")
           .GreaterThan(0).WithMessage("OrderPayId должен быть положительным числом.");
    }
}
