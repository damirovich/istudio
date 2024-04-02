
namespace ISTUDIO.Application.Features.FavoriteProducts.Commands;

public class CreateFavoriteProductsCommandValidator : AbstractValidator<CreateFavoriteProductsCommand>
{
    public CreateFavoriteProductsCommandValidator()
    {
        RuleFor(v => v.ProductId).NotEmpty().WithMessage("ProductId не должен быть пустым.")
          .GreaterThan(0).WithMessage("ProductId должен быть положительным числом.");

        RuleFor(v => v.UserId).NotEmpty().WithMessage("UserId не должен быть пустым.");
    }
}
