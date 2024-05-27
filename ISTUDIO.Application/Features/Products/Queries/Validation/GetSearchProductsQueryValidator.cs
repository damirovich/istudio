namespace ISTUDIO.Application.Features.Products.Queries.Validation;

public class GetSearchProductsQueryValidator : AbstractValidator<GetSearchProductsQuery>
{
    public GetSearchProductsQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .NotNull().WithMessage("Параметры пагинации и поиска не должны быть пустыми.")
            .SetValidator(new PaginationWithSearchParametersValidator());
    }
}