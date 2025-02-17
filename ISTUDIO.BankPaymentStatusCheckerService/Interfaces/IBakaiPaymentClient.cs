using ISTUDIO.Domain.Models.BakaiPay;

namespace ISTUDIO.BankPaymentStatusCheckerService.Interfaces;

public interface IBakaiPaymentClient
{
    Task<BakaiPayCheckStatusResModel> CheckStatusPay(int paymentId);    
}
