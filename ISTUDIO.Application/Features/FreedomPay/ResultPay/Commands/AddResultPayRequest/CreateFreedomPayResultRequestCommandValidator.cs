namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;

public class CreateFreedomPayResultRequestCommandValidator : AbstractValidator<CreateFreedomPayResultRequestCommand>
{
    public CreateFreedomPayResultRequestCommandValidator()
    {
        RuleFor(v => v.JsonData).NotEmpty().WithMessage("Json not null");
        //RuleFor(v => v.PgOrderId).NotEmpty().WithMessage("Order ID is required.");
        //RuleFor(v => v.PgPaymentId).NotEmpty().WithMessage("Payment ID is required.");
        //RuleFor(v => v.PgAmount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        //RuleFor(v => v.PgCurrency).NotEmpty().WithMessage("Currency is required.");
        //RuleFor(v => v.PgDescription).MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
    }
}
