namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddResponseInitPay;

public class CreateFreedomPayInitResponseCommandValidator : AbstractValidator<CreateFreedomPayInitResponseCommand>
{
    public CreateFreedomPayInitResponseCommandValidator()
    {
        RuleFor(v => v.Status).NotEmpty().WithMessage("Status is required.");
        RuleFor(v => v.PaymentId).NotEmpty().WithMessage("Payment ID is required.");
        RuleFor(v => v.RedirectUrl).MaximumLength(500).WithMessage("Redirect URL cannot be longer than 500 characters.");
        RuleFor(v => v.RedirectUrlType).MaximumLength(50).WithMessage("Redirect URL Type cannot be longer than 50 characters.");
        RuleFor(v => v.Salt).NotEmpty().WithMessage("Salt is required.");
        RuleFor(v => v.Sig).NotEmpty().WithMessage("Signature is required.");
        RuleFor(v => v.ResultUrl).MaximumLength(500).WithMessage("Result URL cannot be longer than 500 characters.");
    }
}
