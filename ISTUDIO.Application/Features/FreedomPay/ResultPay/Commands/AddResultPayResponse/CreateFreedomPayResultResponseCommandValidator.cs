namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayResponse;

public class CreateFreedomPayResultResponseCommandValidator : AbstractValidator<CreateFreedomPayResultResponseCommand>
{
    public CreateFreedomPayResultResponseCommandValidator()
    {
        RuleFor(v => v.Status).NotEmpty().WithMessage("Status is required.");
        RuleFor(v => v.Description).MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
        RuleFor(v => v.Salt).NotEmpty().WithMessage("Salt is required.");
        RuleFor(v => v.Sig).NotEmpty().WithMessage("Signature is required.");
    }
}
