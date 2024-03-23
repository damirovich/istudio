using Azure;
using ISTUDIO.Domain.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ISTUDIO.Infrastructure.Services.Integrations;

public class SmsNikitaService : ISmsNikitaService
{
    private readonly HttpClient _httpClient;

    public SmsNikitaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<SmsNikitaResponseModel> SendSms(SmsNikitaRequestModel model)
    {
        
        var xmlString = SerializeToXml(model);
        var content = new StringContent(xmlString, Encoding.UTF8, "application/xml");

        var resHttp = await _httpClient.PostAsync(_httpClient.BaseAddress, content);

        if (resHttp.IsSuccessStatusCode)
        {
            var responseContent = await resHttp.Content.ReadAsStringAsync();
            var serializer = new XmlSerializer(typeof(SmsNikitaResponseModel));
            using (var reader = new StringReader(responseContent))
            {
                try
                {
                    var respDeserialize = (SmsNikitaResponseModel)serializer.Deserialize(reader);

                    return new SmsNikitaResponseModel
                    {
                        Id = respDeserialize.Id,
                        Status = respDeserialize.Status,
                        Phones = respDeserialize.Phones,
                        SmsCount = respDeserialize.SmsCount,
                        Message = respDeserialize.Message,
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка десериализации: {ex.Message}");
                    return new SmsNikitaResponseModel();
                }
            }
        }
        else
        {
            // Обработка ошибочного ответа, если требуется
            return new SmsNikitaResponseModel { Status = 0, Message = "Failed to send SMS" };
        }
    }
    
    private string SerializeToXml(object obj)
    {
        var xmlSerializer = new XmlSerializer(obj.GetType());

        // Убираем пространства имен XML Schema и XML Schema Instance из сериализации
        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        using (var stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, obj, namespaces);
            return stringWriter.ToString();
        }
    }
}
