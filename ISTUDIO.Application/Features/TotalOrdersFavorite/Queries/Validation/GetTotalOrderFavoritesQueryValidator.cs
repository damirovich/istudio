namespace ISTUDIO.Application.Features.TotalOrdersFavorite.Queries.Validation;
public class GetTotalOrderFavoritesQueryValidator : AbstractValidator<GetTotalOrderFavoritesQuery>
{
    public GetTotalOrderFavoritesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId обязательное поле.")
            .Length(1, 250).WithMessage("UserId пользователя должен содержать от 1 до 250 символов.");
    }
}
