using ISTUDIO.Contracts.Features.BakaiPay;
using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService.Models;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class BakaiPayController : BaseController
{
    private readonly IBakaiPayApiClient _apiClient;
    private readonly ILogger<BakaiPayController> _logger;

    public BakaiPayController(IBakaiPayApiClient apiClient, ILogger<BakaiPayController> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
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
    public Task<IActionResult> CreatePay([FromBody, Required] BakaiCreatePayVM request)
    {
        if (!ModelState.IsValid)
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        var requestBakaiPay = new BakaiPayCreateOperationReqModel()
        {
            PhoneNumber = request.PhoneNumber,
            Amount = request.SumProducts,
            PaymentCode = request.OrderId.ToString(),
            OrderId = request.OrderId.ToString()
        };

        return HandleRequestAsync(() => _apiClient.PayCreate(requestBakaiPay));
    }

    //[HttpPost("confirm")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public Task<IActionResult> PayConfirm([FromBody, Required] BakaiConfirmPayVMy  request)
    //{
    //    if (!ModelState.IsValid)
    //        return Task.FromResult<IActionResult>(BadRequest(ModelState));

    //    var requestModel = new BakaiPayConfirmOperReqModel()
    //    {
    //        Id = request.Id,
    //        Otp = request.OTP
    //    };

    //    return HandleRequestAsync(() => _apiClient.PayConfirm(requestModel));
    //}

    [HttpPost("confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PayConfirm([FromBody, Required] BakaiConfirmPayVMy request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var requestModel = new BakaiPayConfirmOperReqModel()
        {
            Id = request.Id,
            Otp = request.OTP
        };

        try
        {
            // Шаг 1: Подтвердить платеж
            var confirmResult = await _apiClient.PayConfirm(requestModel);

            // Шаг 2: Ожидать изменения статуса
            var statusSucceeded = await WaitForPaymentStatusAsync(confirmResult.Id);

            if (statusSucceeded)
            {
                return Ok(new { Success = true, Message = "Платеж успешно подтвержден." });
            }
            else
            {
                return Ok(new { Success = false, Message = "Платеж не был успешным." });
            }
        }
        catch (BadRequestException ex)
        {
            // Обработка 400 Bad Request
            return BadRequest(new { Error = ex.Message });
        }
        catch (UnprocessableEntityException ex)
        {
            // Обработка 422 Unprocessable Entity
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (ApiException ex)
        {
            // Обработка других API ошибок
            return StatusCode(ex.StatusCode, new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            // Обработка неожиданных ошибок
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An unexpected error occurred." });
        }
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<bool> WaitForPaymentStatusAsync(int payId, int timeoutInSeconds = 60, int pollingInterval = 5)
    {
        var startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < timeoutInSeconds)
        {
            try
            {
                // Проверяем статус платежа
                var statusResult = await _apiClient.CheckStatusPay(payId);

                if (Enum.TryParse(statusResult.Status, true, out PaymentStatus status))
                {
                    switch (status)
                    {
                        case PaymentStatus.Executed:
                            _logger.LogInformation("Платеж успешно выполнен.");
                            return true;

                        case PaymentStatus.Rejected:
                            _logger.LogWarning("Платеж отклонен.");
                            return false;

                        case PaymentStatus.Expired:
                            _logger.LogWarning("Время ожидания подтверждения истекло.");
                            return false;

                        case PaymentStatus.Waiting:
                        case PaymentStatus.Processing:
                            _logger.LogInformation("Статус платежа: {Status}. Продолжаем ожидание...", status);
                            break;

                        default:
                            _logger.LogError("Неизвестный статус: {Status}", status);
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке статуса платежа PayId: {PayId}", payId);
                return false;
            }

            // Ждем перед следующим запросом
            await Task.Delay(pollingInterval * 1000);
        }

        _logger.LogWarning("Таймаут ожидания статуса истек.");
        return false;
    }



}
