namespace ISTUDIO.Application.Features.Products.Queries.Validation;

public class GetProductsByIdQueryValidator : AbstractValidator<GetProductsByIdQuery>
{
    public GetProductsByIdQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть больше 0.");
    }
}