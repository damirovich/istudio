namespace ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;

public class UpdateStatusOrdersCommandValidator : AbstractValidator<UpdateStatusOrdersCommand>
{
    public UpdateStatusOrdersCommandValidator()
    {
        RuleFor(v => v.OrderId)
          .NotEmpty().WithMessage("OrderId не должен быть пустым.")
          .GreaterThan(0).WithMessage("OrderId должен быть положительным числом.");

        RuleFor(v => v.OrderStatus)
            .NotEmpty().WithMessage("Status не должен быть пустым.");
    }
}
