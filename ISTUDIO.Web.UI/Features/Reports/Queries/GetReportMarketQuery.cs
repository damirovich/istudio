using System.Net;
using System.Net.Http;

namespace ISTUDIO.Web.UI.Features.Reports.Queries;
using ResModel = String;

public class GetReportMarketQuery : IRequest<ResModel>
{
    public string reportName { get; set; }

    public class Handler : IRequestHandler<GetReportMarketQuery, ResModel>
    {
        private readonly HttpClient _httpClient;

        public Handler(IHttpClientFactory httpClientFactory)
        {

            // Создаем HttpClientHandler с NTLM аутентификацией
            var handler = new HttpClientHandler
            {
                // Указываем конкретные учетные данные для NTLM аутентификации
                Credentials = new NetworkCredential("repmarketkg", "123qweASD", "WIN-2019")
                // Если у вас нет домена, можно просто опустить параметр домена.
            };
            // Создаем HttpClient через HttpClientFactory, но передаем наш кастомный HttpClientHandler
            _httpClient = httpClientFactory.CreateClient("IstudioReport");
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = _httpClient.BaseAddress // Переносим BaseAddress из созданного через HttpClientFactory клиента
            };
        }

        public async Task<ResModel> Handle(GetReportMarketQuery request, CancellationToken cancellationToken)
        {
            // Формирование адреса запроса к SSRS
            var address = _httpClient.BaseAddress + request.reportName;

            return  address;
            //// Отправка HTTP запроса с NTLM аутентификацией
            //var resHttp = await _httpClient.GetAsync(address, cancellationToken);

            //// Проверка успешности ответа
            //if (resHttp.IsSuccessStatusCode)
            //{
            //    // Чтение содержимого ответа
            //    var responseContent = await resHttp.Content.ReadAsStringAsync(cancellationToken);

            //    // Возвращаем содержимое
            //    return responseContent;
            //}
            //else
            //{
            //    // В случае ошибки возвращаем сообщение об ошибке
            //    return $"Ошибка: {resHttp.StatusCode}, Причина: {resHttp.ReasonPhrase}";
            //}
        }
    }
}
