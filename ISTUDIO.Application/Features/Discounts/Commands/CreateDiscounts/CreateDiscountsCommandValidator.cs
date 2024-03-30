namespace ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

public class CreateDiscountsCommandValidator : AbstractValidator<CreateDiscountsCommand>
{
    public CreateDiscountsCommandValidator()
    {
        RuleFor(x => x.PercenTage)
          .NotEmpty().WithMessage("Percentage is required.")
          .GreaterThan(0).WithMessage("Percentage must be greater than 0.")
          .LessThanOrEqualTo(100).WithMessage("Percentage must be less than or equal to 100.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.")
            .Must(BeAValidDateTime).WithMessage("Invalid start time.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must(BeAValidDateTime).WithMessage("Invalid end time.")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be greater than start time.");
    }
    private bool BeAValidDateTime(DateTime dateTime)
    {
        // Customize this method according to your specific DateTime validation logic
        return dateTime != default;
    }
}
