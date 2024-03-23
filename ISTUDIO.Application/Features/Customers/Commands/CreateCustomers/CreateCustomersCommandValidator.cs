using FluentValidation;

namespace ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;

internal class CreateCustomersCommandValidator : AbstractValidator<CreateCustomersCommand>
{
    public CreateCustomersCommandValidator()
    {
        RuleFor(command => command.PIN)
            .NotEmpty().WithMessage("PIN is required.")
            .Length(14).WithMessage("PIN must be between 14 characters.");

        RuleFor(command => command.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(command => command.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters.");

        RuleFor(command => command.Patronymic)
            .MaximumLength(50).WithMessage("Patronymic cannot exceed 50 characters.");

        RuleFor(command => command.Sex)
            .MaximumLength(10).WithMessage("Sex cannot exceed 10 characters.");

        RuleFor(command => command.Nationality)
            .MaximumLength(50).WithMessage("Nationality cannot exceed 50 characters.");

        RuleFor(command => command.DateOfBirth)
            .NotNull().WithMessage("Date of birth is required.");

        RuleFor(command => command.SeriesNumDocument)
            .MaximumLength(50).WithMessage("Series/number document cannot exceed 50 characters.");

        RuleFor(command => command.DateOfExpiry)
            .NotNull().WithMessage("Date of expiry is required.");

        RuleFor(command => command.PlaceOfBirth)
            .NotNull().WithMessage("Place Of Birth  is required.");

        RuleFor(command => command.Authority)
            .MaximumLength(100).WithMessage("Authority cannot exceed 100 characters.");

        RuleFor(command => command.DateOfIssue)
            .NotNull().WithMessage("Date of issue is required.");

        RuleFor(command => command.Ethnicity)
            .MaximumLength(50).WithMessage("Ethnicity cannot exceed 50 characters.");

        RuleFor(command => command.Address)
            .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");

        // Добавьте сообщения для UserId и CustomerImages, если необходимо
    }
}
