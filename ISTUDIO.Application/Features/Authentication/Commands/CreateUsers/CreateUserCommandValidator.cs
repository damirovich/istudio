namespace ISTUDIO.Application.Features.Authentication.Commands.CreateUsers;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.FirstName).NotEmpty();
        RuleFor(v => v.LastName).NotEmpty();
        RuleFor(v => v.Email).NotEmpty();
        RuleFor(v => v.UserName).NotEmpty();
        RuleFor(v => v.Password).NotEmpty();
        RuleFor(v => v.Roles).NotEmpty();
    }
}
