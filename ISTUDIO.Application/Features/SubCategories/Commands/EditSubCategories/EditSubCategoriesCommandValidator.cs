namespace ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;

public class EditSubCategoriesCommandValidator : AbstractValidator<EditSubCategoriesCommand>
{
    public EditSubCategoriesCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id  не должен быть пустым.")
         .GreaterThan(0).WithMessage("Id должен быть положительным числом.");

        RuleFor(v => v.Name)
          .NotEmpty().WithMessage("Имя Под Категории обязательно.")
          .MaximumLength(100).WithMessage("Имя Под Категории не должно превышать 100 символов.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Описание Под Категории обязательно.");

        RuleFor(v => v.CategoryId).NotEmpty().WithMessage("CategoryId категории не должен быть пустым.")
          .GreaterThan(0).WithMessage("CategoryId  должен быть положительным числом.");
    }
}
