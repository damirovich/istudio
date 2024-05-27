namespace ISTUDIO.Application.Features.Orders.Queries.Validation;

public class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId не должен быть пустым.")
            .MaximumLength(50).WithMessage("UserId не должен превышать 50 символов.");
    }
}