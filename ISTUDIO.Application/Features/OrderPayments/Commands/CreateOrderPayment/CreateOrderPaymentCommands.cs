namespace ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

public class CreateOrderPaymentCommands : IRequest<Result>
{
    public string UserId { get; set; }
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Amount { get; set; }    
    public decimal? DebitBonusAmount { get; set; }
    public decimal? CreditBonusAmount { get; set; }
    public string? StatusPayment { get; set; }
    public string? ReceiptPhoto { get; set; }

}
