using ISTUDIO.Application.Common.Models;
using ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
using MediatR;
namespace ISTUDIO.BankPaymentStatusCheckerService.Banks.BakaiBank;


public class BakaiPaymentStatusChecker : IPaymentStatusChecker
{
    private readonly IOrderServices _orderService;
    private readonly IBakaiPaymentClient _bakaiPaymentClient;
    private readonly ILogger<BakaiPaymentStatusChecker> _logger;
    private readonly IAppUserService _appUserService;
    private readonly IMediator _mediator;

    public BakaiPaymentStatusChecker(IOrderServices orderService, IBakaiPaymentClient bakaiPaymentClient, ILogger<BakaiPaymentStatusChecker> logger, IAppUserService appUserService, IMediator mediator)
    {
        _orderService = orderService;
        _bakaiPaymentClient = bakaiPaymentClient;
        _logger = logger;
        _appUserService = appUserService;
        _mediator = mediator;
    }

    public async Task CheckAndUpdatePaymentsAsync()
    {
        var orders = await _orderService.GetOrdersWithStatusAsync("OrderPaymentVerification");

        

        foreach (var order in orders)
        {
            var user = await _appUserService.GetUserDetailsByUserIdAsync(order.UserId);
            if (order.PaymentMethod == "bakai")
            {
                var paymentStatus = await _bakaiPaymentClient.CheckStatusPay(order.CreateTranId);

                if (paymentStatus.Status == "EXECUTED")
                {
                    await _orderService.UpdateStatusOrderPay(order.OrderId, "OrderPaid");
                    var smsMessage = new CreateSmsNikitaReqCommand
                    {
                        PhonesNumber = string.IsNullOrWhiteSpace(user.UserPhoneNumber)
                                                                ? new List<string> { "996704933737", "996703202622" }
                                                                : new List<string> { user.UserPhoneNumber },
                        Message = $"Ваш заказ #{order.OrderId} успешно оплачен через Бакай Банк. Спасибо за покупку!"
                    };

                    // Отправка SMS
                    await _mediator.Send(smsMessage);
                    _logger.LogInformation("Заказ {OrderId} успешно оплачен через Бакай Банк.", order.OrderId);
                }
                else if (paymentStatus.Status == "REJECTED")
                {
                    await _orderService.UpdateStatusOrderPay(order.OrderId, "OrderRejected");
                    var smsMessage = new CreateSmsNikitaReqCommand
                    {
                        PhonesNumber = string.IsNullOrWhiteSpace(user.UserPhoneNumber)
                                                                ? new List<string> { "996704933737", "996703202622" }
                                                                : new List<string> { user.UserPhoneNumber },
                        Message = $"Ваш заказ #{order.OrderId} отклонен при оплате через Бакай Банк. Попробуйте снова"
                    };

                    await _mediator.Send(smsMessage);
                    _logger.LogInformation("Заказ {OrderId} платеж отклонен через Бакай Банк.", order.OrderId);
                }
                else if (paymentStatus.Status == "EXPIRED")
                {
                    await _orderService.UpdateStatusOrderPay(order.OrderId, "EXPIRED");
                    var smsMessage = new CreateSmsNikitaReqCommand
                    {
                        PhonesNumber = string.IsNullOrWhiteSpace(user.UserPhoneNumber)
                                                               ? new List<string> { "996704933737", "996703202622" }
                                                               : new List<string> { user.UserPhoneNumber },
                        Message = $"Ваш заказ #{order.OrderId} не был оплачен вовремя через Бакай Банк. Повторите попытку оплаты."
                    };

                    await _mediator.Send(smsMessage);
                    _logger.LogInformation("Заказ {OrderId} истекло время ожидания подтверждения Бакай Банк.", order.OrderId);
                }
               
            }
        }
    }
}
