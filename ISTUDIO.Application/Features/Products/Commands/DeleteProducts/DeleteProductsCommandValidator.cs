namespace ISTUDIO.Application.Features.Products.Commands.DeleteProducts;

public class DeleteProductsCommandValidator : AbstractValidator<DeleteProductsCommand>
{
    public DeleteProductsCommandValidator()
    {
        RuleFor(v => v.ProductId).NotEmpty().WithMessage("ProductId не должен быть пустым.")
        .GreaterThan(0).WithMessage("ProductId должен быть положительным числом.");
    }
}
