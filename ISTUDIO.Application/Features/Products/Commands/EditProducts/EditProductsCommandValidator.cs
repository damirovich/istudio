namespace ISTUDIO.Application.Features.Products.Commands.EditProducts;

public class EditProductsCommandValidator : AbstractValidator<EditProductsCommand>
{
    public EditProductsCommandValidator() 
    {
        RuleFor(x => x.Id)
           .NotNull().WithMessage("Идентификатор категории не может быть пустым.")
            .GreaterThan(0).WithMessage("CategoryId должен быть больше нуля.");

        RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Имя продукта не может быть пустым.")
               .MaximumLength(100).WithMessage("Имя продукта должно быть не длиннее 100 символов.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Модель продукта не может быть пустой.")
            .MaximumLength(50).WithMessage("Модель продукта должна быть не длиннее 50 символов.");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Цвет продукта не может быть пустым.")
            .MaximumLength(20).WithMessage("Цвет продукта должен быть не длиннее 20 символов.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше нуля.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Количество продукта на складе не может быть отрицательным.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Описание продукта должно быть не длиннее 500 символов.");

        RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("CategoryId  не может быть пустым.")
            .GreaterThan(0).WithMessage("CategoryId должен быть больше нуля.");
    }
}
