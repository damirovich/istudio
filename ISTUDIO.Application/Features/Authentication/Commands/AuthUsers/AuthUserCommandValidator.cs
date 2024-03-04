namespace ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;

public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
{
    public AuthUserCommandValidator()
    {
        RuleFor(v => v.UserName).NotEmpty();
        RuleFor(v => v.Password).NotEmpty();
    }
}
