namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

public class CreateOrdersCommandValidator : AbstractValidator<CreateOrdersCommand>
{
    public CreateOrdersCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("UserId не должен быть пустым.");

        RuleFor(v => v.TotalAmount)
            .GreaterThan(0).WithMessage("TotalAmount должен быть больше нуля.");

        RuleFor(v => v.TotalQuantyProduct)
            .GreaterThan(0).WithMessage("TotalQuantyProduct должен быть больше нуля.");

        RuleFor(v => v.ProductOrders)
            .NotEmpty().WithMessage("ProductOrders не должен быть пустым.");
    }
}
