using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
namespace ISTUDIO.BankPaymentStatusCheckerService.Banks.BakaiBank;


public class BakaiPaymentStatusChecker : IPaymentStatusChecker
{
    private readonly IOrderServices _orderService;
    private readonly IBakaiPaymentClient _bakaiPaymentClient;
    private readonly ILogger<BakaiPaymentStatusChecker> _logger;

    public BakaiPaymentStatusChecker(IOrderServices orderService, IBakaiPaymentClient bakaiPaymentClient, ILogger<BakaiPaymentStatusChecker> logger)
    {
        _orderService = orderService;
        _bakaiPaymentClient = bakaiPaymentClient;
        _logger = logger;
    }

    public async Task CheckAndUpdatePaymentsAsync()
    {
        var orders = await _orderService.GetOrdersWithStatusAsync("OrderPaymentVerification");

        foreach (var order in orders)
        {
            if (order.PaymentMethod == "bakai")
            {
                var paymentStatus = await _bakaiPaymentClient.CheckStatusPay(order.CreateTranId);

                if (paymentStatus.Status == "EXECUTED")
                {
                    await _orderService.UpdateStatusOrderPay(order.OrderId, "OrderPaid");
                    _logger.LogInformation("Заказ {OrderId} успешно оплачен через Бакай Банк.", order.OrderId);
                }
            }
        }
    }
}
