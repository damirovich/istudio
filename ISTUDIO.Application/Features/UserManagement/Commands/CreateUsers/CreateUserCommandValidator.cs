namespace ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email не должен быть пустым.")
            .EmailAddress().WithMessage("Неправильный формат адреса электронной почты.");
        RuleFor(v => v.PhoneNumber).NotEmpty().WithMessage("Имя пользователя не должно быть пустым.");
        RuleFor(v => v.Password).NotEmpty().WithMessage("Пароль не должен быть пустым.")
            .MinimumLength(8).WithMessage("Пароль должен содержать как минимум 8 символов.")
            .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
            .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
            .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
            .Matches("[!@#$%^&*]").WithMessage("Пароль должен содержать хотя бы один специальный символ: !@#$%^&*");

        RuleFor(v => v.HasAgreedToPrivacyPolicy)
          .NotEmpty().WithMessage("HasAgreedToPrivacyPolicy обязательное поле.");

        RuleFor(v => v.ConsentToTheUserAgreement)
             .NotEmpty().WithMessage("ConsentToTheUserAgreement обязательное поле.");

        RuleFor(v => v.Roles).NotEmpty().WithMessage("Список ролей не должен быть пустым.")
            .Must(roles => roles.Count >= 1).WithMessage("Список ролей должен содержать как минимум одну роль.");
    }
}
