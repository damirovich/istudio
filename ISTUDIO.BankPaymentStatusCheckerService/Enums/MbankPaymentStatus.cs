namespace ISTUDIO.BankPaymentStatusCheckerService.Enums;

public enum MbankPaymentStatus
{
    New,         // Новый
    Processing,  // В обработке
    Completed,   // Завершен
    Canceled,    // Отменен
    Failed       // Ошибка
}
