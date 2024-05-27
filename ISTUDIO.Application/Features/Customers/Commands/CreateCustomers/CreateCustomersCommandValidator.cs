using FluentValidation;

namespace ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;

internal class CreateCustomersCommandValidator : AbstractValidator<CreateCustomersCommand>
{
    public CreateCustomersCommandValidator()
    {
        RuleFor(v => v.PIN)
             .NotEmpty().WithMessage("PIN обязателен для заполнения.")
             .Length(14).WithMessage("PIN должен состоять из 14 символов.");

        RuleFor(v => v.FullName)
            .NotEmpty().WithMessage("Полное имя обязательно для заполнения.")
            .MaximumLength(100).WithMessage("Полное имя не должно превышать 100 символов.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Полное имя должно содержать только буквы.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Имя обязательно для заполнения.")
            .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Имя должно содержать только буквы.");

        RuleFor(v => v.Surname)
            .NotEmpty().WithMessage("Фамилия обязательна для заполнения.")
            .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Фамилия должна содержать только буквы.");

        RuleFor(v => v.Patronymic)
            .MaximumLength(50).WithMessage("Отчество не должно превышать 50 символов.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]*$").WithMessage("Отчество должно содержать только буквы.");

        RuleFor(v => v.Sex)
            .MaximumLength(10).WithMessage("Пол не должен превышать 10 символов.");

        RuleFor(v => v.Nationality)
            .MaximumLength(50).WithMessage("Национальность не должна превышать 50 символов.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]*$").WithMessage("Национальность должно содержать только буквы.");

        RuleFor(v => v.DateOfBirth)
            .NotNull().WithMessage("Дата рождения обязательна для заполнения.");

        RuleFor(v => v.SeriesNumDocument)
            .MaximumLength(50).WithMessage("Серия/номер документа не должны превышать 50 символов.");

        RuleFor(v => v.DateOfExpiry)
            .NotNull().WithMessage("Дата окончания срока действия обязательна для заполнения.");

        RuleFor(v => v.PlaceOfBirth)
            .NotEmpty().WithMessage("Место рождения обязательно для заполнения.");

        RuleFor(v => v.Authority)
            .MaximumLength(100).WithMessage("Орган, выдавший документ, не должен превышать 100 символов.");

        RuleFor(v => v.DateOfIssue)
            .NotNull().WithMessage("Дата выдачи обязательна для заполнения.");

        RuleFor(v => v.Ethnicity)
            .MaximumLength(50).WithMessage("Этническая принадлежность не должна превышать 50 символов.");

        RuleFor(v => v.Address)
            .MaximumLength(250).WithMessage("Адрес не должен превышать 250 символов.");


        // Добавьте сообщения для UserId и CustomerImages, если необходимо
    }
}
