using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService.Models;

namespace ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;

public interface IBakaiPayApiClient
{
    Task<CheckPropsResModel> PayCheckProps(string phoneNumber);
    Task<BakaiPayCreateOperationResModel> PayCreate(BakaiPayCreateOperationReqModel createReq);
    Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq);
    Task<BakaiPayCheckStatusResModel> CheckStatusPay(int payId);


}
