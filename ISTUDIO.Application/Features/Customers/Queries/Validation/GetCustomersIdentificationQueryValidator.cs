namespace ISTUDIO.Application.Features.Customers.Queries.Validation;

public class GetCustomersIdentificationQueryValidator : AbstractValidator<GetCustomersIdentificationQuery>
{
    public GetCustomersIdentificationQueryValidator()
    {
        RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("UserId обязателен для заполнения.")
               .MaximumLength(150).WithMessage("UserId не должен превышать 150 символов.");
    }
}
