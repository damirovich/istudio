namespace ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;

public class UpdateProductQuantityCommandValidator : AbstractValidator<UpdateProductQuantityCommand>
{
    public UpdateProductQuantityCommandValidator()
    {
        RuleFor(v => v.ProductId)
            .NotNull().WithMessage("Идентификатор продукта не может быть пустым.")
            .GreaterThan(0).WithMessage("ProductId должен быть больше нуля.");

        RuleFor(v => v.ProductQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Количество продукта не может быть отрицательным.");
    }
}