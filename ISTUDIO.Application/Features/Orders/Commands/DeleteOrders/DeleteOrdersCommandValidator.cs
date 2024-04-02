namespace ISTUDIO.Application.Features.Orders.Commands.DeleteOrders;

public class DeleteOrdersCommandValidator : AbstractValidator<DeleteOrdersCommand>  
{
    public DeleteOrdersCommandValidator()
    {
        RuleFor(v => v.OrderId).NotEmpty().WithMessage("OrderId не должен быть пустым.")
           .GreaterThan(0).WithMessage("OrderId должен быть положительным числом.");
    }
}
