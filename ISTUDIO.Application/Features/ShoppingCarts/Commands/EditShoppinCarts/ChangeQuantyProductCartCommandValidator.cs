
namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;

public class ChangeQuantyProductCartCommandValidator : AbstractValidator<ChangeQuantyProductCartCommand>
{
    public ChangeQuantyProductCartCommandValidator()
    {
        RuleFor(v => v.CartId).NotEmpty().WithMessage("CartId не должен быть пустым.")
            .GreaterThan(0).WithMessage("CartId должен быть положительным числом.");

        RuleFor(v => v.QuantyProduct).NotEmpty().WithMessage("QuantyProduct не должен быть пустым.")
            .GreaterThan(0).WithMessage("QuantyProduct должен быть положительным числом.");
    }
}
