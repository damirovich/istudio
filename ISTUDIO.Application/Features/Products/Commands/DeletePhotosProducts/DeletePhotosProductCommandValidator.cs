using FluentValidation.Validators;

namespace ISTUDIO.Application.Features.Products.Commands.DeletePhotosProducts;

public class DeletePhotosProductCommandValidator : AbstractValidator<DeletePhotosProductCommand>
{
    public DeletePhotosProductCommandValidator()
    {
        RuleFor(v => v.ProductImagesId).NotEmpty().WithMessage("ProductImagesId не должен быть пустым.")
      .GreaterThan(0).WithMessage("ProductImagesId должен быть положительным числом.");
    }
}
