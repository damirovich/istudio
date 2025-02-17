namespace ISTUDIO.BankPaymentStatusCheckerService.Interfaces;

public interface IPaymentStatusChecker
{
    Task CheckAndUpdatePaymentsAsync();
}
