
namespace ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;

public class AddProductToCartsCommandValidator : AbstractValidator<AddProductToCartsCommand>
{
    public AddProductToCartsCommandValidator()
    {

        RuleFor(x => x.UserId)
          .NotEmpty().WithMessage("UserId не может быть пустой.")
          .MaximumLength(200).WithMessage("UserId должна быть не длиннее 200 символов.");

        RuleFor(v => v.ProductId).NotEmpty().WithMessage("ProductId не должен быть пустым.")
          .GreaterThan(0).WithMessage("ProductId должен быть положительным числом.");
    }
}
