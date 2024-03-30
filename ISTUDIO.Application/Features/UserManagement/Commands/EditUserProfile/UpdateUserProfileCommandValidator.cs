namespace ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

public class UpdateUserProfileCommandValidator : AbstractValidator<EditUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty().WithMessage("User Id не должен быть пустым.");
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email не должен быть пустым.")
            .EmailAddress().WithMessage("Неправильный формат адреса электронной почты.");
        RuleFor(v => v.PhoneNumber).NotEmpty().WithMessage("Имя пользователя не должно быть пустым.");
    }
}