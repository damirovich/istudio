namespace ISTUDIO.Application.Features.OrderAddress.Queries.Validation;

public class GetOrderAddressByIdQueryValidator : AbstractValidator<GetOrderAddressByIdQuery>
{
    public GetOrderAddressByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть больше 0.");
    }
}