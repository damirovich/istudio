namespace ISTUDIO.Application.Features.FavoriteProducts.Queries.Validation;

public class GetFavoriteProductsByUserIdQueryValidator : AbstractValidator<GetFavoriteProductsByUserIdQuery>
{
    public GetFavoriteProductsByUserIdQueryValidator() 
    {
        RuleFor(v => v.UserId)
          .NotEmpty().WithMessage("UserId не должен быть пустым.");
    }
}