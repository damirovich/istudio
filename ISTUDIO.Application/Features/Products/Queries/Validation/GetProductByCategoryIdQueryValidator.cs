namespace ISTUDIO.Application.Features.Products.Queries.Validation;

public class GetProductByCategoryIdQueryValidator : AbstractValidator<GetProductByCategoryIdQuery>
{
    public GetProductByCategoryIdQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId должен быть больше 0.");

        RuleFor(x => x.Parameters)
            .NotNull().WithMessage("Параметры пагинации не должны быть пустыми.")
            .SetValidator(new PaginatedParametersValidator());
    }
}