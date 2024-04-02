namespace ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;

public class DeleteCategoriesCommandValidator : AbstractValidator<DeleteCategoriesCommand>
{
    public DeleteCategoriesCommandValidator()
    {
        RuleFor(v => v.CategoryId).NotEmpty().WithMessage("CategoryId категории не должен быть пустым.")
            .GreaterThan(0).WithMessage("CategoryId категории должен быть положительным числом.");
        
    }
}
