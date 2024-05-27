namespace ISTUDIO.Application.Features.UserManagement.Queries.Validation;

public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
{
    public GetUserByNameQueryValidator()
    {
        RuleFor(v => v.UserName).
          NotEmpty().WithMessage("UserName Не должен быть пустым")
          .MaximumLength(250).WithMessage("UserName не должен превышать 250 символов");
    }
}
