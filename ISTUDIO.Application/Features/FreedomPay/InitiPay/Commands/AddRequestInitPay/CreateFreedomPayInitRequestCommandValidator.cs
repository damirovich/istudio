namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;

public class CreateFreedomPayInitRequestCommandValidator : AbstractValidator<CreateFreedomPayInitRequestCommand>
{
    public CreateFreedomPayInitRequestCommandValidator()
    {
        RuleFor(v => v.PgOrderId).NotEmpty().WithMessage("Order ID is required.");
        RuleFor(v => v.PgMerchantId).NotEmpty().WithMessage("Merchant ID is required.");
        RuleFor(v => v.PgAmount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(v => v.PgDescription).MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
        RuleFor(v => v.PgSalt).NotEmpty().WithMessage("Salt is required.");
        RuleFor(v => v.PgSig).NotEmpty().WithMessage("Signature is required.");
    }
}