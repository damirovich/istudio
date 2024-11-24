using ISTUDIO.Domain.Models.BakaiPay;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IBakaiPayService
{
    /// <summary>
    /// Проверка реквизита в Банке Check Props
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<bool> PayCheckProps(string phoneNumber);

    /// <summary>
    /// Создание платежа Create Transaction
    /// </summary>
    /// <param name="createReq"></param>
    /// <returns></returns>
    Task<BakaiPayCreateOperationResModel> PayCreate(BakaiPayCreateOperationReqModel createReq);

    /// <summary>
    /// Подтверждение платеже Confirm Transaction
    /// </summary>
    /// <param name="confirmReq"></param>
    /// <returns></returns>
    Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq);

    /// <summary>
    /// Проверка статуса платежа Check Status
    /// </summary>
    /// <param name="payId"></param>
    /// <returns></returns>
    Task<BakaiPayCheckStatusResModel> CheckStatusPay(int payId);
}
