using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;

namespace ISTUDIO.Contracts.Features.OrderPayments;

public class EditOrderPaymentVM : IMapWith<EditOrderPaymentCommands>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string? TransactionId { get; set; }
    //Фото чека
    public string? ReceiptPhoto { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditOrderPaymentVM, EditOrderPaymentCommands>();
    }
}
