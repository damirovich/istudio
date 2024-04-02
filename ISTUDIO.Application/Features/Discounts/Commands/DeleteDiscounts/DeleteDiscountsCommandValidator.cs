namespace ISTUDIO.Application.Features.Discounts.Commands.DeleteDiscounts;

public class DeleteDiscountsCommandValidator : AbstractValidator<DeleteDiscountsCommand>
{
    public DeleteDiscountsCommandValidator()
    {
        RuleFor(v => v.DiscountId).NotEmpty().WithMessage("DiscountId не должен быть пустым.")
           .GreaterThan(0).WithMessage("DiscountId должен быть положительным числом.");
    }
}
