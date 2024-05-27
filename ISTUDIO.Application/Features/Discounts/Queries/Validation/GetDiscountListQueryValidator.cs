namespace ISTUDIO.Application.Features.Discounts.Queries.Validation;

public class GetDiscountListQueryValidator : AbstractValidator<GetDiscountListQuery>
{
    public GetDiscountListQueryValidator()
    {
        RuleFor(x => x.Parameters)
          .NotNull().WithMessage("Parameters не должны быть пустыми.")
          .SetValidator(new PaginatedParametersValidator());
    }
}

