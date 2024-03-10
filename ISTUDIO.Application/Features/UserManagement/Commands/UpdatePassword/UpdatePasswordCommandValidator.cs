namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty().WithMessage("Идентификатор пользователя не должен быть пустым.");
        RuleFor(v => v.OldPassword).NotEmpty().WithMessage("Старый пароль не должен быть пустым.");
        RuleFor(v => v.NewPassword).NotEmpty().WithMessage("Пароль не должен быть пустым.")
          .MinimumLength(8).WithMessage("Пароль должен содержать как минимум 8 символов.")
          .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
          .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
          .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
          .Matches("[!@#$%^&*]").WithMessage("Пароль должен содержать хотя бы один специальный символ: !@#$%^&*");
    }
}
