
using ISTUDIO.Application.Features.Products.Commands.UpdateProductIsActive;

namespace ISTUDIO.Application.Features.Products.Commands.UpdateProductActive;

public class UpdateProductActiveCommandValidator : AbstractValidator<UpdateProductActiveCommand>
{
    public UpdateProductActiveCommandValidator()
    {
        RuleFor(v => v.ProductId)
             .NotNull().WithMessage("Идентификатор продукта не может быть пустым.")
             .GreaterThan(0).WithMessage("ProductId должен быть больше нуля.");
        RuleFor(v => v.ProductActive)
            .NotNull().When(v => v.ProductActive == null).WithMessage("Статус продукта не может быть Null");
    }
}
