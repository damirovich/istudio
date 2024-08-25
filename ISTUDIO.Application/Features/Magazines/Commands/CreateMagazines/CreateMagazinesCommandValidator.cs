namespace ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;

public class CreateMagazinesCommandValidator : AbstractValidator<CreateMagazinesCommand>
{
    public CreateMagazinesCommandValidator()
    {
        RuleFor(v => v.Name)
              .NotEmpty().WithMessage("Название Магазина не может быть пустым.")
              .MaximumLength(200).WithMessage("Название Магазина должно быть не длиннее 200 символов.");

        //RuleFor(v => v.Description)
        //    .NotEmpty().WithMessage("Описание Магазина не может быть пустым.")
        //    .MaximumLength(5000).WithMessage("Описание журнала должно быть не длиннее 5000 символов.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Адрес Магазина не может быть пустым.")
            .MaximumLength(300).WithMessage("Адрес Магазина должен быть не длиннее 300 символов.");

        RuleFor(v => v.PhoneNumber)
            .NotEmpty().WithMessage("Телефонный номер Магазина не может быть пустым.")
            .Matches("^[+]?[0-9]{10,15}$").WithMessage("Телефонный номер журнала должен быть действительным и содержать от 10 до 15 цифр.");

        //RuleFor(v => v.PhotoLogoURL)
        //    .NotEmpty().WithMessage("URL логотипа журнала не может быть пустым.")
        //    .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("URL логотипа журнала должен быть действительным URL.");

        //RuleFor(v => v.UserId)
        //    .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым.");
    }
}