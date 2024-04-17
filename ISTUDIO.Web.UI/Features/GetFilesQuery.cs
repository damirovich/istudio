namespace ISTUDIO.Web.UI.Features;
using ISTUDIO.Domain.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ResModel = File;
public class GetFilesQuery : IRequest<ResModel>
{
    public string imageUrl { get; set; }
    public class Handler : IRequestHandler<GetFilesQuery, ResModel>
    {
        private readonly HttpClient _httpClient;
        public Handler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomHttpClient");
        }
        public async Task<ResModel> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var address = _httpClient.BaseAddress + "FilesStore/GetFile?photoFilePath=" + request.imageUrl;
            var resHttp = await _httpClient.GetAsync(address);

            if (resHttp.IsSuccessStatusCode)
            {
                var responseContent = await resHttp.Content.ReadAsStringAsync();
                var file = JsonSerializer.Deserialize<ResModel>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });
                return file;
            }
            else
            {
                return new ResModel();
            }
        }
    }
}

public class File {
    [JsonPropertyName("File")]
    public string FileBase64 { get; set; }
}