namespace ISTUDIO.Application.Features.Products.Commands.EditProducts;

public class EditProductsCommandValidator : AbstractValidator<EditProductsCommand>
{
    public EditProductsCommandValidator() 
    {
        RuleFor(v => v.Id)
           .NotNull().WithMessage("Идентификатор категории не может быть пустым.")
            .GreaterThan(0).WithMessage("CategoryId должен быть больше нуля.");

        RuleFor(v => v.Name)
               .NotEmpty().WithMessage("Имя продукта не может быть пустым.")
               .MaximumLength(100).WithMessage("Имя продукта должно быть не длиннее 100 символов.");

        RuleFor(v => v.Model)
            .NotEmpty().WithMessage("Модель продукта не может быть пустой.")
            .MaximumLength(100).WithMessage("Модель продукта должна быть не длиннее 50 символов.");

        RuleFor(v => v.Color)
            .NotEmpty().WithMessage("Цвет продукта не может быть пустым.")
            .MaximumLength(50).WithMessage("Цвет продукта должен быть не длиннее 20 символов.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше нуля.");

        RuleFor(v => v.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Количество продукта на складе не может быть отрицательным.");

        RuleFor(v => v.Description)
            .MaximumLength(5000).WithMessage("Описание продукта должно быть не длиннее 5000 символов.");

        RuleFor(v => v.CategoryId)
            .NotNull().WithMessage("CategoryId  не может быть пустым.")
            .GreaterThan(0).WithMessage("CategoryId должен быть больше нуля.");

        RuleFor(v => v.MagazineId)
            .NotNull().WithMessage("MagazineId не должен быть пустым.")
            .GreaterThan(0).WithMessage("MagazineId должен быть больше нуля.");
    }
}
