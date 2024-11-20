using ISTUDIO.Domain.Models; 
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ISTUDIO.Infrastructure.Services.Integrations;

public class FreedomPayServices : IFreedomPayService
{
    private readonly HttpClient _httpClient;
    private readonly string _secretKeyInitPay;
    private readonly string _secretKeyResPay;
    private readonly string _baseAddress;

    public FreedomPayServices(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _secretKeyInitPay = configuration["FreedomPay:InitSecretKey"];
        _secretKeyResPay = configuration["FreedomPay:ResSecretKey"];
        _baseAddress = configuration["FreedomPay:BaseAddresFreedomPay"];
    }

    public async Task<FreedomPayInitResponseModel> SendFreedomPay(FreedomPayInitRequestModel requestModel)
    {
        // Генерация подписи
        requestModel.PgSig = GenerateSignatureInitPay(requestModel);
        var formData = new MultipartFormDataContent
        {
            { new StringContent(requestModel.PgOrderId), "pg_order_id" },
            { new StringContent(requestModel.PgMerchantId.ToString()), "pg_merchant_id" },
            { new StringContent(requestModel.PgAmount.ToString()), "pg_amount" },
            { new StringContent(requestModel.PgDescription), "pg_description" },
            { new StringContent(requestModel.PgResultUrl), "pg_result_url" },
            { new StringContent(requestModel.PgSalt), "pg_salt" },
            { new StringContent(requestModel.PgSig), "pg_sig" }
        };

        var resHttp = await _httpClient.PostAsync(_baseAddress, formData);

        if (resHttp.IsSuccessStatusCode)
        {
            var responseContent = await resHttp.Content.ReadAsStringAsync();
            var serializer = new XmlSerializer(typeof(FreedomPayInitResponseModel));
            using var reader = new StringReader(responseContent);
            try
            {
                return (FreedomPayInitResponseModel)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка десериализации: {ex.Message}");
                return new FreedomPayInitResponseModel();
            }
        }
        else
        {
            throw new Exception("Failed to initiate payment");
        }
    }

    public async Task<FreedomPayResultResponseModel> ReturnReusltFreedomPay(FreedomPayResultRequestModel requestModel)
    {
        // Проверка подписи
        //var generatedSignature = GenerateSignatureResRequest(requestModel);
        //if (!generatedSignature.Equals(requestModel.PgSig, StringComparison.OrdinalIgnoreCase))
        //{
        //    throw new Exception("Invalid signature");
        //}

        if (requestModel["pg_result"] == "1")
        {
            return CreateResponseModel("ok", "Заказ оплачен", requestModel["pg_salt"]);
        }
        else if (requestModel["pg_can_reject"] == "1")
        {
            return CreateResponseModel("rejected", "Платеж отменен", requestModel["pg_salt"]);
        }
        else
        {
            return CreateResponseModel("error", "Ошибка в интерпретации данных", requestModel["pg_salt"]);
        }
    }

    private string GenerateSignatureInitPay(FreedomPayInitRequestModel requestModel)
    {
        var data = $"init_payment.php;{requestModel.PgAmount};{requestModel.PgDescription};{requestModel.PgMerchantId};{requestModel.PgOrderId};{requestModel.PgResultUrl};{requestModel.PgSalt};{_secretKeyInitPay}";
        return GenerateMd5Hash(data);
    }

    //private string GenerateSignatureResRequest(FreedomPayResultRequestModel requestModel)
    //{
    //    var data = $"{requestModel.PgOrderId};{requestModel.PgPaymentId};{requestModel.PgAmount};{requestModel.PgCurrency};{requestModel.PgSalt};{_secretKeyResPay}";
    //    return GenerateMd5Hash(data);
    //}

    private string GenerateResponseSignature(string status, string salt)
    {
        var data = $"{status};{salt};{_secretKeyInitPay}";
        return GenerateMd5Hash(data);
    }

    private string GenerateMd5Hash(string input)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
    }

    private FreedomPayResultResponseModel CreateResponseModel(string status, string description, string salt)
    {
        return new FreedomPayResultResponseModel
        {
            PgStatus = status,
            PgDescription = description,
            PgSalt = salt,
            PgSig = GenerateResponseSignature(status, salt)
        };
    }
   
}


