namespace ISTUDIO.Application.Features.OrderPayments.Commands.AddReceptPhotoPayment;

public class AddReceipPhotoOrderPaymentCommandValidator : AbstractValidator<AddReceipPhotoOrderPaymentCommand>
{
    public AddReceipPhotoOrderPaymentCommandValidator()
    {
        RuleFor(x => x.OrdersId)
            .GreaterThan(0).WithMessage("OrderId Должен быть больше 0.");

        RuleFor(x => x.ReceiptPhoto)
            .NotEmpty().WithMessage("ReceiptPhoto обязательное поле.");
    }
}
