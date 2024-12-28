using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService.Models;
using System.Net;
using NotFoundException = ISTUDIO.Domain.Models.BakaiPay.NotFoundException;

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
            await HandleErrorResponse(response);
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayCheckStatusResModel>();
    }

    public async Task<CheckPropsResModel> PayCheckProps(string phoneNumber)
    {
        var response = await _httpClient.GetAsync($"api/v1/BakaiPay/PayCheckProps/check-props?phoneNumber={phoneNumber}");

        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response);
        }

        return await response.Content.ReadFromJsonAsync<CheckPropsResModel>();
    }

    public async Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/BakaiPay/PayConfirm/confirm", confirmReq);

        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response);
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayConfirmOperResModel>();
    }

    public async Task<BakaiPayCreateOperationResModel> PayCreate(BakaiPayCreateOperationReqModel createReq)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/BakaiPay/PayCreate/create", createReq);

        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponse(response);
        }

        return await response.Content.ReadFromJsonAsync<BakaiPayCreateOperationResModel>();
    }

    private async Task HandleErrorResponse(HttpResponseMessage response)
    {
        string errorMessage;

        // Проверяем, содержит ли тело ошибки ожидаемую структуру
        if (response.Content.Headers.ContentType?.MediaType == "application/json")
        {
            // Пытаемся десериализовать в разные возможные структуры
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequestAPI>();
                errorMessage = errorResponse?.Error ?? "Bad request occurred.";
                throw new BadRequestException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
                errorMessage = errorResponse?.Detail?.FirstOrDefault()?.Msg ?? "Validation error occurred.";
                throw new UnprocessableEntityException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequestAPI>();
                errorMessage = errorResponse?.Error ?? "Resource not found.";
                throw new NotFoundException(errorMessage);
            }
        }

        // Если структура ответа неизвестна
        errorMessage = response.ReasonPhrase ?? "An unexpected error occurred.";
        throw new ApiException(errorMessage, (int)response.StatusCode);
    }
}
