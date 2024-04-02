namespace ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

public class CreateSubCategoriesCommandValidator : AbstractValidator<CreateSubCategoriesCommand>
{
    public CreateSubCategoriesCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Имя Под Категории обязательно.")
            .MaximumLength(100).WithMessage("Имя Под Категории не должно превышать 100 символов.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Описание Под Категории обязательно.");

        RuleFor(v => v.CategoryId).NotEmpty().WithMessage("CategoryId категории не должен быть пустым.")
          .GreaterThan(0).WithMessage("CategoryId  должен быть положительным числом.");
    }
}
