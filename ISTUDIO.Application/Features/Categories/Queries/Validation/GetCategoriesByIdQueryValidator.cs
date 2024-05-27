
namespace ISTUDIO.Application.Features.Categories.Queries.Validation;

public class GetCategoriesByIdQueryValidator : AbstractValidator<GetCategoriesByIdQuery>
{
    public GetCategoriesByIdQueryValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id категории не должен быть пустым.")
          .GreaterThan(0).WithMessage("Id категории должен быть положительным числом.");

    }
}
