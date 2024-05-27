namespace ISTUDIO.Application.Features.ShoppingCarts.Queries.Validation;

public class GetShoppingCartsByUserIdValidator : AbstractValidator<GetShoppingCartsByUserId>
{
    public GetShoppingCartsByUserIdValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId не должен быть пустым.")
            .MaximumLength(50).WithMessage("UserId не должен превышать 50 символов.");
    }
}
