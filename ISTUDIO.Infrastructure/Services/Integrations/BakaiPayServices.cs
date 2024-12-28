using ISTUDIO.Domain.Models.BakaiPay;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;

namespace ISTUDIO.Infrastructure.Services.Integrations;

public class BakaiPayServices : IBakaiPayService
{
    private readonly HttpClient _httpClient;
    private readonly string UserName;
    private readonly string Password;
    
    public BakaiPayServices (HttpClient httpClient, IConfiguration configuration)
    {

        var handler = new HttpClientHandler
        {
            SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13
        };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(configuration["BakaiPay:BaseAddresBakaiPay"])
        };
        UserName = configuration["BakaiPay:UserName"]; 
        Password = configuration["BakaiPay:Password"]; 
    }

    /// <summary>
    /// Проверка статуса платежа Check Status
    /// </summary>
    /// <param name="payId"></param>
    /// <returns></returns>
    public async Task<BakaiPayCheckStatusResModel> CheckStatusPay(int payId)
    {
        AddBasicAuthenticationHeader();
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/payment/status?id={payId}");

        if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BakaiPayCheckStatusResModel>();
        }
        else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
            throw new UnprocessableEntityException(errorResponse?.Detail?[0]?.Msg ?? "Unprocessable Entity");
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequest>();
            throw new BadRequestExceptionBakai(errorResponse?.Details ?? "Bad Request");
        }
        else
        {
            throw new ApiException($"Unexpected status code: {response.StatusCode}", (int)response.StatusCode);
        }

    }
    /// <summary>
    /// Проверка реквизита в Банке Check Props
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<bool> PayCheckProps(string phoneNumber)
    {
        // Добавление заголовка авторизации
        AddBasicAuthenticationHeader();

        try
        {
            // Выполняем запрос
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/payment/check?phone={phoneNumber}");

            // Если запрос успешный или статус 201 Created
            if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BakaiPayCheckPropsResModel>();
                return result?.Message ?? false;
            }

            // Если статус 422 Unprocessable Entity
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
                throw new UnprocessableEntityException(errorResponse?.Detail?[0]?.Msg ?? "Unprocessable Entity");
            }

            // Если статус 404 Not Found
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequest>();
                throw new Domain.Models.BakaiPay.NotFoundException(errorResponse?.Details ?? "Resource not found");
            }

            // Если другие статусы
            throw new ApiException($"Unexpected status code: {response.StatusCode}", (int)response.StatusCode);
        }
        catch (UnprocessableEntityException ex)
        {
            // Логируем исключение, если необходимо
            Console.WriteLine($"Validation error: {ex.Message}");
            throw;
        }
        catch (Domain.Models.BakaiPay.NotFoundException ex)
        {
            // Логируем исключение, если необходимо
            Console.WriteLine($"Not Found error: {ex.Message}");
            throw;
        }
        catch (ApiException ex)
        {
            // Логируем исключение, если необходимо
            Console.WriteLine($"API error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Логируем неожиданные исключения
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApiException("An unexpected error occurred. Please try again later.", 500);
        }

    }

    /// <summary>
    /// Подтверждение платежа Confirm Transaction
    /// </summary>
    /// <param name="confirmReq"></param>
    /// <returns></returns>
    public async Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq)
    {
        AddBasicAuthenticationHeader();

        try
        {
            // Отправка POST-запроса
            var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/payment/confirm", confirmReq);

            // Успешный запрос (200 OK)
            if (response.StatusCode == HttpStatusCode.OK || response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BakaiPayConfirmOperResModel>();
            }

            // Обработка 400 Bad Request
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequest>();
                throw new BadRequestExceptionBakai(errorResponse?.Details ?? "Bad request occurred.");
            }

            // Обработка 422 Unprocessable Entity
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
                throw new UnprocessableEntityException(errorResponse?.Detail?[0]?.Msg ?? "Unprocessable entity error occurred.");
            }

            // Обработка других статусов
            throw new ApiException($"Unexpected status code: {response.StatusCode}", (int)response.StatusCode);
        }
        catch (BadRequestExceptionBakai ex)
        {
            // Логирование для 400 Bad Request
            Console.WriteLine($"Bad request error: {ex.Message}");
            throw; // Повторно выбрасываем для обработки в контроллере
        }
        catch (UnprocessableEntityException ex)
        {
            // Логирование для 422 Unprocessable Entity
            Console.WriteLine($"Unprocessable entity error: {ex.Message}");
            throw; // Повторно выбрасываем для обработки в контроллере
        }
        catch (ApiException ex)
        {
            // Логирование других API-ошибок
            Console.WriteLine($"API error: {ex.Message}");
            throw; // Повторно выбрасываем для обработки в контроллере
        }
        catch (Exception ex)
        {
            // Логирование неожиданных ошибок
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApiException("An unexpected error occurred during payment confirmation.", 500);
        }
        
    }

    /// <summary>
    /// Создание платежа Create Transaction
    /// </summary>
    /// <param name="createReq"></param>
    /// <returns></returns>
    public async Task<BakaiPayCreateOperationResModel> PayCreate(BakaiPayCreateOperationReqModel createReq)
    {
        AddBasicAuthenticationHeader();

        try
        {
            // Отправка POST-запроса
            var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/payment/create", createReq);

            // Успешный запрос (201 Created)
            if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BakaiPayCreateOperationResModel>();
            }

            // Обработка 400 Bad Request
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ValidBadRequest>();
                throw new BadRequestExceptionBakai(errorResponse?.Details ?? "Bad request occurred during payment creation.");
            }

            // Обработка 422 Unprocessable Entity
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
                throw new UnprocessableEntityException(errorResponse?.Detail?[0]?.Msg ?? "Unprocessable entity error during payment creation.");
            }

            // Обработка других статусов
            throw new ApiException($"Unexpected status code: {response.StatusCode}", (int)response.StatusCode);
        }
        catch (UnprocessableEntityException ex)
        {
            // Логирование ошибки
            Console.WriteLine($"Validation error: {ex.Message}");
            throw;
        }
        catch (BadRequestExceptionBakai ex)
        {
            // Логирование ошибки
            Console.WriteLine($"Bad request error: {ex.Message}");
            throw;
        }
        catch (ApiException ex)
        {
            // Логирование API-ошибки
            Console.WriteLine($"API error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Логирование неожиданной ошибки
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApiException("An unexpected error occurred during payment creation.", 500);
        }
    }


    /// <summary>
    /// Аавторизация через метод Basic
    /// </summary>
    private void AddBasicAuthenticationHeader()
    {
        var byteArray = System.Text.Encoding.ASCII.GetBytes($"{UserName}:{Password}");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

}
