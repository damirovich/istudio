namespace ISTUDIO.Web.Api.BakaiPay.Models;

public enum PaymentStatus
{
    Waiting,     // при создании
    Processing,  // после подтверждения ОТП
    Executed,    // платеж исполнен
    Rejected,    // платеж отклонен
    Expired      // истекло время ожидания подтверждения
}
