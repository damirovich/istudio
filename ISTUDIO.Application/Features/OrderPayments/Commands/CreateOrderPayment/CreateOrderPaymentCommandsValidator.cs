namespace ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

public class CreateOrderPaymentCommandsValidator : AbstractValidator<CreateOrderPaymentCommands>
{
    public CreateOrderPaymentCommandsValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId должен быть больше 0.");

        RuleFor(x => x.PaymentMethodId)
            .GreaterThan(0).WithMessage("PaymentMethodId должен быть больше 0.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Сумма платежа должна быть больше 0.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Статус обязателен для заполнения.")
            .MaximumLength(50).WithMessage("Статус не должен превышать 50 символов.");

        RuleFor(x => x.TransactionId)
            .MaximumLength(100).WithMessage("TransactionId не должен превышать 100 символов.");

        RuleFor(x => x.ReceiptPhoto)
            .Must(BeAValidBase64).When(x => !string.IsNullOrEmpty(x.ReceiptPhoto))
            .WithMessage("Фото чека должно быть в формате Base64.");
    }

    private bool BeAValidBase64(string? base64)
    {
        if (string.IsNullOrEmpty(base64)) return true;
        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
