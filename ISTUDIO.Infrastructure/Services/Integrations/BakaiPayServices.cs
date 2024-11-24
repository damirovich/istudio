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
        response.EnsureSuccessStatusCode();
        if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BakaiPayCheckStatusResModel>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorResponse?.Detail?[0]?.Msg}");
        }
        else
        {
            throw new HttpRequestException($"Request failed with status {response.StatusCode}");
        }

    }
    /// <summary>
    /// Проверка реквизита в Банке Check Props
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<bool> PayCheckProps(string phoneNumber)
    {
        AddBasicAuthenticationHeader();
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/payment/check?phone={phoneNumber}");
        response.EnsureSuccessStatusCode();
        if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<BakaiPayCheckPropsResModel>();
            return result?.Message ?? false;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorResponse?.Detail?[0]?.Msg}");
        }
        else
        {
            throw new HttpRequestException($"Request failed with status {response.StatusCode}");
        }

    }

    /// <summary>
    /// Подтверждение платеже Confirm Transaction
    /// </summary>
    /// <param name="confirmReq"></param>
    /// <returns></returns>
    public async Task<BakaiPayConfirmOperResModel> PayConfirm(BakaiPayConfirmOperReqModel confirmReq)
    {
        AddBasicAuthenticationHeader();
        var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/payment/confirm", confirmReq);
        response.EnsureSuccessStatusCode();
        if(response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BakaiPayConfirmOperResModel>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorResponse?.Detail?[0]?.Msg}");
        }
        else
        {
            throw new HttpRequestException($"Request failed with status {response.StatusCode}");
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
        var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/payment/create", createReq);
        //response.EnsureSuccessStatusCode();
        if (response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BakaiPayCreateOperationResModel>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<BakaiPayValidationErrorResponse>();
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorResponse?.Detail?[0]?.Msg}");
        }
        else
        {
            throw new HttpRequestException($"Request failed with status {response.StatusCode}");
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
