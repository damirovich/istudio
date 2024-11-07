using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IFreedomPayService
{
    Task<FreedomPayInitResponseModel> SendFreedomPay(FreedomPayInitRequestModel requestModel);
    Task<FreedomPayResultResponseModel> ReturnReusltFreedomPay(FreedomPayResultRequestModel requestModel);
}
