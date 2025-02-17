namespace ISTUDIO.BankPaymentStatusCheckerService.Enums;

public enum BakaiPaymentStatus
{
    Pending,     // Ожидание
    InProgress,  // Обрабатывается
    Success,     // Успешно
    Fail,        // Отклонено
    Timeout      // Истекло время
}
