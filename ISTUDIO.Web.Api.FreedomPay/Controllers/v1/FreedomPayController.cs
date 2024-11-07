
using ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;
using ISTUDIO.Domain.Models;
using System.Globalization;

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

            var result = await _freedomPayService.SendFreedomPay(requestModel);

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
    public async Task<IActionResult> ResultPayment([FromForm] FreedomPayResultRequestModel requestModel)
    {
        if (requestModel == null)
        {
            _logger.LogWarning("Received null request in ResultPayment");
            return BadRequest("Invalid result payment request data");
        }

        try 
        {
            _logger.LogInformation("Processing ResultPayment for PgOrderId: {PgOrderId}", requestModel.PgOrderId);

            var result = await _freedomPayService.ReturnReusltFreedomPay(requestModel);

            var addTable = await Mediator.Send(new CreateFreedomPayResultRequestCommand()
            {
                PgOrderId = Convert.ToInt32(requestModel.PgOrderId),
                PgPaymentId = Convert.ToInt32(requestModel.PgPaymentId),
                PgAmount = decimal.Parse(requestModel.PgPsAmount, CultureInfo.InvariantCulture),
                PgCurrency = requestModel.PgCurrency,
                PgNetAmount = decimal.Parse(requestModel.PgNetAmount, CultureInfo.InvariantCulture),
                PgPsAmount = decimal.Parse(requestModel.PgPsAmount, CultureInfo.InvariantCulture),
                PgPsFullAmount = decimal.Parse(requestModel.PgPsFullAmount, CultureInfo.InvariantCulture),
                PgPsCurrency = requestModel.PgCurrency,
                PgDescription = requestModel.PgDescription,
                PgResult = (int)requestModel.PgResult,
                PgPaymentDate = Convert.ToDateTime(requestModel.PgPaymentDate),
                PgCanReject = (int)requestModel.PgCanReject,
                PgUserPhone = requestModel.PgUserPhone,
                PgNeedPhoneNotification = (short)requestModel.PgNeedPhoneNotification,
                PgUserContactEmail = requestModel.PgUserContactEmail,
                PgNeedEmailNotification = (short)requestModel.PgNeedEmailNotification,
                PgTestingMode = (int)requestModel.PgTestingMode,
                PgCaptured = (int)requestModel.PgCaptured,
                PgReference = requestModel.PgReference,
                PgCardPan = requestModel.PgCardPan,
                PgAuthCode = requestModel.PgAuthCode,
                PgSalt = requestModel.PgSalt,
                PgSig = requestModel.PgSig,
                PgPaymentMethod = requestModel.PgPaymentMethod
            });
            _logger.LogInformation("ResultPayment processed successfully for PgOrderId: {PgOrderId}", requestModel.PgOrderId);
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing ResultPayment for PgOrderId: {PgOrderId}", requestModel.PgOrderId);
            return StatusCode(500, ex.Message);
        }
    }
}
