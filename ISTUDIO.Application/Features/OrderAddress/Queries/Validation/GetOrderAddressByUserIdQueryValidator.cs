namespace ISTUDIO.Application.Features.OrderAddress.Queries.Validation;

public class GetOrderAddressByUserIdQueryValidator : AbstractValidator<GetOrderAddressByUserIdQuery>
{
    public GetOrderAddressByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId не должен быть пустым.")
            .MaximumLength(250).WithMessage("UserId не должен превышать 250 символов.");
    }
}