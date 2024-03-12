namespace ISTUDIO.Application.Features.SubCategories.Commands.DeleteSubCategories;

public class DeleteSubCategoriesCommandValidator : AbstractValidator<DeleteSubCategoriesCommand>
{
    public DeleteSubCategoriesCommandValidator()
    {
        RuleFor(v => v.SubCategoryId).NotEmpty().WithMessage("SubCategoryId  не должен быть пустым.")
           .GreaterThan(0).WithMessage("SubCategoryId должен быть положительным числом.");
    }
}
