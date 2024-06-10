namespace ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;

public class AddReceipPhotoOrdersCommandValidator : AbstractValidator<AddReceipPhotoOrdersCommand>
{
    public AddReceipPhotoOrdersCommandValidator()
    {
        RuleFor(x => x.OrdersId)
            .GreaterThan(0).WithMessage("OrderId Должен быть больше 0.");

        RuleFor(x => x.ReceiptPhoto)
            .NotEmpty().WithMessage("ReceiptPhoto обязательное поле.");
    }
}