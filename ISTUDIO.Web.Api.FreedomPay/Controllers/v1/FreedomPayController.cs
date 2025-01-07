
//using ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;
using ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;
using ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddResponseInitPay;
using ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;
using ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayResponse;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;
using ISTUDIO.Domain.Models;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace ISTUDIO.Web.Api.FreedomPay.Controllers.v1;

[ApiVersion("1.0")]
public class FreedomPayController : BaseController
{
    private readonly ILogger<FreedomPayController> _logger;
    private readonly IFreedomPayService _freedomPayService;

    public FreedomPayController(IFreedomPayService freedomPayService, ILogger<FreedomPayController> logger)
    {
        _freedomPayService = freedomPayService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> InitiatePayment([FromBody] FreedomPayInitRequestModel requestModel)
    {
        if (requestModel == null)
        {
            _logger.LogWarning("Received null request in InitiatePayment");
            return BadRequest("Invalid payment request data");
        }

        try
        {
            _logger.LogInformation("Processing InitiatePayment for RequestId: {RequestId}", requestModel.PgOrderId);

            var addRequestRes = await Mediator.Send(new CreateFreedomPayInitReqCommand()
            {
                JsonData = JsonSerializer.Serialize(requestModel)
            });

            var result = await _freedomPayService.SendFreedomPay(requestModel);

            var resInitPay = await Mediator.Send(new CreateFreedomPayInitResponseCommand()
            {
                Status = result.PgStatus,
                PaymentId = result.PgPaymentId,
                RedirectUrl = result.PgRedirectUrl,
                RedirectUrlType = result.PgRedirectUrlType,
                Salt = result.PgSalt,
                Sig = result.PgSig
            });

            _logger.LogInformation("InitiatePayment processed successfully for RequestId: {RequestId}", requestModel.PgOrderId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing InitiatePayment for RequestId: {RequestId}", requestModel.PgOrderId);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> ResultPayment([FromForm] Dictionary<string, string> requestForm)
    {
        if (requestForm == null || requestForm.Count == 0)
        {
            _logger.LogWarning("Получен null или пустой запрос в ResultPayment");
            return BadRequest("Invalid result payment request data");
        }

        try
        {
            _logger.LogInformation("Обработка ResultPayment Для PgOrderId: {PgOrderId}", requestForm.ContainsKey("pg_order_id") ? requestForm["pg_order_id"] : "N/A");

            var requestModel = new FreedomPayResultRequestModel();
            foreach (var parameter in requestForm)
            {
                requestModel.AddParameter(parameter.Key, parameter.Value);
            }

            requestModel.ValidateRequiredFields();

            var result = await _freedomPayService.ReturnReusltFreedomPay(requestModel);
            var xml = SerializeResponseToXml(result);

            var addRequestRes = await Mediator.Send(new CreateFreedomPayResultRequestCommand()
            {
                JsonData = requestModel.ToJson()
            });

            var addResReqResult = await Mediator.Send(new CreateFreedomPayResultResponseCommand()
            {
                Status = result.PgStatus,
                Description = result.PgDescription,
                Salt = result.PgSalt,
                Sig = result.PgSig
            });

            if (result.PgStatus == "ok")
            {
                if (int.TryParse(requestModel["pg_order_id"], out var orderId))
                {
                    var upStatusOrder = await Mediator.Send(new UpdateStatusOrdersCommand
                    {
                        OrderId = orderId,
                        OrderStatus = "OrderPaid"
                    });
                }
                else
                {
                    _logger.LogWarning("Не удалось преобразовать pg_order_id в число: {PgOrderId}", requestModel["pg_order_id"]);
                }

            }

            _logger.LogInformation("ResultPayment успешно обработан для PgOrderId: {PgOrderId}", requestModel["pg_order_id"]);
            return Content(xml, "application/xml");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обработке ResultPayment для PgOrderId: {PgOrderId}", requestForm.ContainsKey("pg_order_id") ? requestForm["pg_order_id"] : "N/A");
            return StatusCode(500, ex.Message);
        }
    }


    private string SerializeResponseToXml(FreedomPayResultResponseModel responseModel)
    {
        var xmlSerializer = new XmlSerializer(typeof(FreedomPayResultResponseModel));
        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", ""); // Убираем лишние неймспейсы

        using var stringWriter = new StringWriter();
        using var xmlWriter = System.Xml.XmlWriter.Create(stringWriter, new System.Xml.XmlWriterSettings { OmitXmlDeclaration = true, Encoding = Encoding.UTF8 });

        xmlSerializer.Serialize(xmlWriter, responseModel, namespaces);

        return stringWriter.ToString();
    }

}

