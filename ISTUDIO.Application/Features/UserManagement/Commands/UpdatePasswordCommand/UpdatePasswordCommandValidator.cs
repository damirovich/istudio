namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdatePasswordCommand;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.OldPassword)
            .MaximumLength(250)
            .NotEmpty();
        RuleFor(v => v.NewPassword)
            .MaximumLength(250)
            .NotEmpty();
    }
}
