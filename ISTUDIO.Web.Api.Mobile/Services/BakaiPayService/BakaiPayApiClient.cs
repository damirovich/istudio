using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService.Models;

namespace ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;

public class BakaiPayApiClient : IBakaiPayApiClient
{
    private readonly HttpClient _httpClient;

    public BakaiPayApiClient(HttpClient httpClient)
        => _httpClient = httpClient;

    public async Task<BakaiPayCheckStatusResModel> CheckStatusPay(int payId)
    {
        var response = await _httpClient.GetAsync($"api/v1/BakaiPay/CheckStatusPay/check-status?payId={payId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to check payment status: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayCheckStatusResModel>();
    }

    public async Task<CheckPropsResModel> PayCheckProps(string phoneNumber)
    {
        var response = await _httpClient.GetAsync($"api/v1/BakaiPay/PayCheckProps/check-props?phoneNumber={phoneNumber}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to check payment properties: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<CheckPropsResModel>();
    }

    public async Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/BakaiPay/PayConfirm/confirm", confirmReq);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to confirm payment: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayConfirmOperResModel>();
    }

    public async Task<BakaiPayCreateOperationResModel> PayCreate(BakaiPayCreateOperationReqModel createReq)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/BakaiPay/PayCreate/create", createReq);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to create payment: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayCreateOperationResModel>();
    }
}
