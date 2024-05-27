namespace ISTUDIO.Application.Features.Orders.Queries.Validation;

public class GetOrderDetailsByIdQueryValidator : AbstractValidator<GetOrderDetailsByIdQuery>
{
    public GetOrderDetailsByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId должен быть больше 0.");
    }
}
