using ISTUDIO.Domain.Models;

namespace ISTUDIO.Web.Api.Mobile.Services.FreedomPayServices;

public interface IFreedomPayApiClient
{
    Task<FreedomPayInitResponseModel> InitiatePaymentAsync(FreedomPayInitRequestModel requestModel);
}
