namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;

public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.FullName)
            .MaximumLength(250)
            .NotEmpty();
    }
}