namespace ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;

public class EditOrderUserAddressCommandValidator : AbstractValidator<EditOrderUserAddressCommand>
{
    public EditOrderUserAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Region)
            .NotEmpty().WithMessage("Region is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

        RuleFor(x => x.Comments)
            .MaximumLength(500).WithMessage("Comments must not exceed 500 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .MaximumLength(50).WithMessage("UserId must not exceed 50 characters.");
    }
}
