namespace ISTUDIO.Application.Features.Customers.Commands.EditCustomers;

internal class EditCustomersCommandValidator : AbstractValidator<EditCustomersCommand>
{
    public EditCustomersCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id  не должен быть пустым.")
           .GreaterThan(0).WithMessage("Id должен быть положительным числом.");

        RuleFor(v => v.PIN)
            .NotEmpty().WithMessage("PIN is required.")
            .Length(14).WithMessage("PIN must be between 14 characters.");

        RuleFor(v => v.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(v => v.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters.");

        RuleFor(v => v.Patronymic)
            .MaximumLength(50).WithMessage("Patronymic cannot exceed 50 characters.");

        RuleFor(v => v.Sex)
            .MaximumLength(10).WithMessage("Sex cannot exceed 10 characters.");

        RuleFor(v => v.Nationality)
            .MaximumLength(50).WithMessage("Nationality cannot exceed 50 characters.");

        RuleFor(v => v.DateOfBirth)
            .NotNull().WithMessage("Date of birth is required.");

        RuleFor(v => v.SeriesNumDocument)
            .MaximumLength(50).WithMessage("Series/number document cannot exceed 50 characters.");

        RuleFor(v => v.DateOfExpiry)
            .NotNull().WithMessage("Date of expiry is required.");

        RuleFor(v => v.PlaceOfBirth)
            .NotNull().WithMessage("Place Of Birth  is required.");

        RuleFor(v => v.Authority)
            .MaximumLength(100).WithMessage("Authority cannot exceed 100 characters.");

        RuleFor(v => v.DateOfIssue)
            .NotNull().WithMessage("Date of issue is required.");

        RuleFor(v => v.Ethnicity)
            .MaximumLength(50).WithMessage("Ethnicity cannot exceed 50 characters.");

        RuleFor(v => v.Address)
            .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");

    }
}
