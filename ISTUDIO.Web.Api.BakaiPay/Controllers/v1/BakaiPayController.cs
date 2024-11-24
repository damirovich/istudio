
using ISTUDIO.Domain.Models.BakaiPay;

namespace ISTUDIO.Web.Api.BakaiPay.Controllers.v1;

[ApiVersion("1.0")]
public class BakaiPayController : BaseController
{
    private readonly ILogger<BakaiPayController> _logger;
    private readonly IBakaiPayService _bakaiPayService;

    public BakaiPayController(ILogger<BakaiPayController> logger, IBakaiPayService bakaiPayService)
    {
        _logger = logger;
        _bakaiPayService = bakaiPayService;
    }

    [HttpGet("check-props")]
    public async Task<IActionResult> PayCheckProps([FromQuery] string phoneNumber)
    {
        if (phoneNumber == null)
        {
            _logger.LogWarning("Received null request in PayCheckProps");
            return BadRequest("Invalid phoneNumber request data");
        }

        try
        {
            var result = await _bakaiPayService.PayCheckProps(phoneNumber);
            return Ok(new { result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing PayCheckProps for PhoneNumber: {RequestId}", phoneNumber);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> PayCreate([FromBody] BakaiPayCreateOperationReqModel createReq)
    {
        if (createReq == null)
        {
            _logger.LogWarning("Received null request in PayCreate");
            return BadRequest("Invalid create request data");
        }

        try
        {
            var result = await _bakaiPayService.PayCreate(createReq);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing PayCreate for Request: {Request}", createReq);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> PayConfirm([FromBody] BakaiPayConfirmOperReqModel confirmReq)
    {
        if (confirmReq == null)
        {
            _logger.LogWarning("Received null request in PayConfirm");
            return BadRequest("Invalid confirm request data");
        }

        try
        {
            var result = await _bakaiPayService.PayConfirm(confirmReq);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing PayConfirm for Request: {Request}", confirmReq);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("check-status")]
    public async Task<IActionResult> CheckStatusPay([FromQuery] int payId)
    {
        try
        {
            var result = await _bakaiPayService.CheckStatusPay(payId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing CheckStatusPay for PayId: {PayId}", payId);
            return StatusCode(500, ex.Message);
        }
    }
}
