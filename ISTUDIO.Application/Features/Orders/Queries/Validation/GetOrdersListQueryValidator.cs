namespace ISTUDIO.Application.Features.Orders.Queries.Validation;

public class GetOrdersListQueryValidator : AbstractValidator<GetOrdersListQuery>
{
    public GetOrdersListQueryValidator()
    {
        RuleFor(x => x.Parameters)
        .NotNull().WithMessage("Parameters не должны быть пустыми.")
        .SetValidator(new PaginatedParametersValidator());
    }
}
