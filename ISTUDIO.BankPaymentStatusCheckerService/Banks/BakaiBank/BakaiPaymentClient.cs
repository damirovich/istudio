using ISTUDIO.Application.Common.Exceptions;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
using ISTUDIO.Domain.Models.BakaiPay;
using System.Net.Http.Json;
using System.Net;

namespace ISTUDIO.BankPaymentStatusCheckerService.Banks.BakaiBank;

using NotFoundException = ISTUDIO.Domain.Models.BakaiPay.NotFoundException;
public class BakaiPaymentClient : IBakaiPaymentClient
{
    private readonly HttpClient _httpClient;

    public BakaiPaymentClient(HttpClient httpClient)
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
