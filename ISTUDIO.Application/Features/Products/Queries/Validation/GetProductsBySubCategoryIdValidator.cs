
namespace ISTUDIO.Application.Features.Products.Queries.Validation;

public class GetProductsBySubCategoryIdValidator : AbstractValidator<GetProductsBySubCategoryId>
{
    public GetProductsBySubCategoryIdValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId должен быть больше 0.");
    }
}