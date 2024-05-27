namespace ISTUDIO.Application.Features.Customers.Queries.Validation;

public class GetCustomersListQueryValidator : AbstractValidator<GetCustomersListQuery>
{
    public GetCustomersListQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .NotNull().WithMessage("Parameters не должны быть пустыми.")
            .SetValidator(new PaginatedParametersValidator());
    }
}
