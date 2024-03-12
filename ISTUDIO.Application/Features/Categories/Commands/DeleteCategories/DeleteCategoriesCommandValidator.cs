namespace ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;

public class DeleteCategoriesCommandValidator : AbstractValidator<DeleteCategoriesCommand>
{
    public DeleteCategoriesCommandValidator()
    {
        RuleFor(v => v.CategoryId).NotEmpty().WithMessage("Id категории не должен быть пустым.")
            .GreaterThan(0).WithMessage("Id категории должен быть положительным числом.");
        
    }
}
