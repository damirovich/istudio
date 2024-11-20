using ISTUDIO.Domain.Models;

namespace ISTUDIO.Web.Api.Mobile.Services.FreedomPayServices;

public class FreedomPayApiClient : IFreedomPayApiClient
{
    private readonly HttpClient _httpClient;

    public FreedomPayApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FreedomPayInitResponseModel> InitiatePaymentAsync(FreedomPayInitRequestModel requestModel)
    {
        var response = await _httpClient.PostAsJsonAsync("InitiatePayment", requestModel);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to initiate payment: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<FreedomPayInitResponseModel>();
    }
}