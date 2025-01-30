using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

public class CreateOrderPaymentCommands : IMapWith<OrderPaymentEntity>, IRequest<Result>
{
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string? TransactionId { get; set; }
    //Фото чека
    public string? ReceiptPhoto { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderPaymentCommands, OrderPaymentEntity>();
    }
}
