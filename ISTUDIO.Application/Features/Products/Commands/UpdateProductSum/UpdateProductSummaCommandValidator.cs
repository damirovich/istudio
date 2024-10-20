namespace ISTUDIO.Application.Features.Products.Commands.UpdateProductSum;

public class UpdateProductSummaCommandValidator : AbstractValidator<UpdateProductSummaCommand>
{
    public UpdateProductSummaCommandValidator()
    {
        RuleFor(v => v.ProductId)
            .NotNull().WithMessage("Идентификатор продукта не может быть пустым.")
            .GreaterThan(0).WithMessage("ProductId должен быть больше нуля.");

        RuleFor(v => v.ProductSumma)
            .GreaterThanOrEqualTo(0).WithMessage("Сумма продукта не может быть отрицательным.");
    }
}