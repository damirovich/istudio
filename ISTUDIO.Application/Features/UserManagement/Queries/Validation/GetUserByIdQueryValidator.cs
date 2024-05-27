namespace ISTUDIO.Application.Features.UserManagement.Queries.Validation;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>    
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(v => v.UserId).
            NotEmpty().WithMessage("UserId Не должен быть пустым")
            .MaximumLength(250).WithMessage("UserId не должен превышать 250 символов");
    }
}
