using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class BakaiPayController : BaseController
{
    private readonly IBakaiPayApiClient _apiClient;

    public BakaiPayController(IBakaiPayApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    private async Task<IActionResult> HandleRequestAsync<T>(Func<Task<T>> action)
    {
        try
        {
            var result = await action();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
        }
    }

    [HttpGet("check-props")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetCheckProps([FromQuery, Required, Phone] string phoneNumber)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        return HandleRequestAsync(() => _apiClient.PayCheckProps(phoneNumber));
    }

    [HttpGet("check-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetCheckStatusPay([FromQuery, Required, Range(1, int.MaxValue)] int payId)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        return HandleRequestAsync(() => _apiClient.CheckStatusPay(payId));
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreatePay([FromBody, Required] BakaiPayCreateOperationReqModel request)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        return HandleRequestAsync(() => _apiClient.PayCreate(request));
    }

    [HttpPost("confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> PayConfirm([FromBody, Required] BakaiPayConfirmOperReqModel request)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        return HandleRequestAsync(() => _apiClient.PayConfirm(request));
    }
}
