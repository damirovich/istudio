
namespace ISTUDIO.Application.Features.UserManagement.Queries.Validation;

public class GetUserByPhoneNumberQueryValidator : AbstractValidator<GetUserByPhoneNumberQuery>
{
    public GetUserByPhoneNumberQueryValidator()
    {
        RuleFor(v => v.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber не должен быть пустым.")
           .MaximumLength(20).WithMessage("PhoneNumber не должен превышать 20 символов.")
           .Matches(@"^\d{10,15}$").WithMessage("PhoneNumber должен содержать от 10 до 15 цифр.");

    }
}
