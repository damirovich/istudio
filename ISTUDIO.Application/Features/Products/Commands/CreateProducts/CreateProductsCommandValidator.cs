using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Application.Features.Products.Commands.CreateProducts;

public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
{
    public CreateProductsCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Имя продукта не может быть пустым.")
            .MaximumLength(100).WithMessage("Имя продукта должно быть не длиннее 100 символов.");

        RuleFor(v => v.Model)
            .NotEmpty().WithMessage("Модель продукта не может быть пустой.")
            .MaximumLength(50).WithMessage("Модель продукта должна быть не длиннее 50 символов.");

        RuleFor(v => v.Color)
            .NotEmpty().WithMessage("Цвет продукта не может быть пустым.")
            .MaximumLength(20).WithMessage("Цвет продукта должен быть не длиннее 20 символов.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше нуля.");

        RuleFor(v => v.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Количество продукта на складе не может быть отрицательным.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Описание продукта должно быть не длиннее 500 символов.");

        //RuleFor(x => x.CategoryId)
        //    .NotNull().WithMessage("Идентификатор категории не может быть пустым.")
        //    .GreaterThan(0).WithMessage("Идентификатор категории должен быть больше нуля.");

        RuleFor(v => v.CategoryId)
            .NotNull().WithMessage("Категория продукта не может быть пустой.");

        //RuleFor(x => x.DiscountId)
        //    .NotNull().WithMessage("Скидка на продукт не может быть пустой.");

        //RuleForEach(x => x.Images)
        //    .SetValidator(new ProductImagesDTOValidator())
        //    .WithMessage("Один или несколько элементов в списке изображений недопустимы.");
    }
}

//public class ProductImagesDTOValidator : AbstractValidator<ProductImagesDTO>
//{
//    public ProductImagesDTOValidator()
//    {
//        RuleFor(x => x.Url)
//            .NotEmpty().WithMessage("URL изображения не может быть пустым.")
//            .MaximumLength(255).WithMessage("URL изображения должен быть не длиннее 255 символов.")
//            .Matches(@"\.(jpg|jpeg|png)$").WithMessage("Формат изображения должен быть JPG, JPEG или PNG.");

//        RuleFor(x => x.ImageAltText)
//            .NotEmpty().WithMessage("Alt текст изображения не может быть пустым.")
//            .MaximumLength(100).WithMessage("Alt текст изображения должен быть не длиннее 100 символов.");
//    }
//}