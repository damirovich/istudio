namespace ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

public class UpdateUserProfileCommandValidator : AbstractValidator<EditUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty().WithMessage("User Id не должен быть пустым.");
        RuleFor(v => v.FullName).NotEmpty().WithMessage("ФИО не должно быть пустым.");
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email не должен быть пустым.")
            .EmailAddress().WithMessage("Неправильный формат адреса электронной почты.");
        RuleFor(v => v.UserName).NotEmpty().WithMessage("Имя пользователя не должно быть пустым.");

        RuleFor(v => v.Roles).NotEmpty().WithMessage("Список ролей не должен быть пустым.")
            .Must(roles => roles.Count >= 1).WithMessage("Список ролей должен содержать как минимум одну роль.");

    }
}