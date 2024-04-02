namespace ISTUDIO.Application.Features.Categories.Commands.EditCategories;

public class EditCategoriesCommandValidator : AbstractValidator<EditCategoriesCommand>
{
    public EditCategoriesCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id категории не должен быть пустым.")
           .GreaterThan(0).WithMessage("Id категории должен быть положительным числом.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Имя категории обязательно.")
            .MaximumLength(100).WithMessage("Имя категории не должно превышать 100 символов.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Описание категории обязательно.");
    }
}
