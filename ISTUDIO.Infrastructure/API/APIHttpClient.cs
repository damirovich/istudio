using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ISTUDIO.Infrastructure.API;

public class APIHttpClient
{
    private readonly HttpClient _httpClient;
    private AuthenticationStateProvider _authentication { get; set; }

    public APIHttpClient(HttpClient httpClient, AuthenticationStateProvider authentication)
    {
        _httpClient = httpClient;
        _authentication = authentication;
    }

    public async Task<ApiResponse<T>> PostJsonAsync<T, Req>(Req req, string url)
    {
        try
        {
            var auth = await _authentication.GetAuthenticationStateAsync();
            var accessToken = auth.User.Claims.FirstOrDefault(x => x.Type == "access_jwtToken")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PostAsJsonAsync(url, req);

            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }
    public async Task<ApiResponse<T>> GetJsonAsync<T>(string url)
    {
        try
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(3000);

            var auth = await _authentication.GetAuthenticationStateAsync();
            var accessToken = auth.User.Claims.FirstOrDefault(x => x.Type == "access_jwtToken")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.GetAsync(url);

            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }
    public async Task<ApiResponse<T>> PutJsonAsync<T, Req>(Req req, string url)
    {
        try
        {
            var auth = await _authentication.GetAuthenticationStateAsync();
            var accessToken = auth.User.Claims.FirstOrDefault(x => x.Type == "access_jwtToken")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PutAsJsonAsync(url, req);

            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }
    public async Task<ApiResponse<T>> DeleteJsonAsync<T>(string url)
    {
        try
        {
            var auth = await _authentication.GetAuthenticationStateAsync();
            var accessToken = auth.User.Claims.FirstOrDefault(x => x.Type == "access_jwtToken")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.DeleteAsync(url);

            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
            if (content is not null)
                return content;
        }

        return new ApiResponse<T>
        {
            CsmReturnStatuses = new List<ApiCsmReturnStatus>
            {
                new ApiCsmReturnStatus
                {
                    Status = (int)response.StatusCode,
                    Message = response.IsSuccessStatusCode ? "Сервис временно не доступен" : $"Ошибка: {response.ReasonPhrase}"
                }
            }
        };
    }

    private ApiResponse<T> HandleException<T>(Exception ex)
    {
        return new ApiResponse<T>
        {
            CsmReturnStatuses = new List<ApiCsmReturnStatus>
            {
                new ApiCsmReturnStatus
                {
                    Status = -1,
                    Message = ex.Message
                }
            }
        };
    }
}
