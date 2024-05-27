namespace ISTUDIO.Application.Features.Products.Queries.Validation;

public  class GetProductsListQueryValidator : AbstractValidator<GetProductsListQuery>
{
    public GetProductsListQueryValidator()
    {
        RuleFor(x => x.Parameters)
          .NotNull().WithMessage("Параметры пагинации не должны быть пустыми.")
          .SetValidator(new PaginatedParametersValidator());
    }
}
