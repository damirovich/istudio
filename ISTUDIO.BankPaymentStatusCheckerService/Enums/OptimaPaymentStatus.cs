namespace ISTUDIO.BankPaymentStatusCheckerService.Enums;

public enum OptimaPaymentStatus
{
    Created,     // Создан
    Confirming,  // Подтверждение
    Paid,        // Оплачено
    Declined,    // Отклонено
    Timeout      // Время истекло
}
