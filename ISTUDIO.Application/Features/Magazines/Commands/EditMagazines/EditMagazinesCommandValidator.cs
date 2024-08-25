
namespace ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;

public class EditMagazinesCommandValidator : AbstractValidator<EditMagazinesCommand>
{
    public EditMagazinesCommandValidator()
    {
        RuleFor(v => v.MagazineId).NotEmpty().WithMessage("MagazineId не должен быть пустым.")
          .GreaterThan(0).WithMessage("MagazineId должен быть положительным числом.");

        RuleFor(v => v.Name)
              .NotEmpty().WithMessage("Название Магазина не может быть пустым.")
              .MaximumLength(200).WithMessage("Название Магазина должно быть не длиннее 200 символов.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Адрес Магазина не может быть пустым.")
            .MaximumLength(300).WithMessage("Адрес Магазина должен быть не длиннее 300 символов.");

        RuleFor(v => v.PhoneNumber)
            .NotEmpty().WithMessage("Телефонный номер Магазина не может быть пустым.")
            .Matches("^[+]?[0-9]{10,15}$").WithMessage("Телефонный номер журнала должен быть действительным и содержать от 10 до 15 цифр.");

    }
}
