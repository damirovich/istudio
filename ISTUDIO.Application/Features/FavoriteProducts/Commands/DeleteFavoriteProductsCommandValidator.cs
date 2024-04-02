
namespace ISTUDIO.Application.Features.FavoriteProducts.Commands;

public class DeleteFavoriteProductsCommandValidator : AbstractValidator<DeleteFavoriteProductsCommand>
{
    public DeleteFavoriteProductsCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id не должен быть пустым.")
          .GreaterThan(0).WithMessage("Id должен быть положительным числом.");
    }
}
