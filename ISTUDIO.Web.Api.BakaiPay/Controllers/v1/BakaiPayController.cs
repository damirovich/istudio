
using ISTUDIO.Application.Features.BakaiPay.CheckStatus;
using ISTUDIO.Application.Features.BakaiPay.ConfirmTransaction;
using ISTUDIO.Application.Features.BakaiPay.CreateTransaction;
using ISTUDIO.Domain.Models.BakaiPay;

namespace ISTUDIO.Web.Api.BakaiPay.Controllers.v1;

[ApiVersion("1.0")]
public class BakaiPayController : BaseController
{
    private readonly ILogger<BakaiPayController> _logger;
    private readonly IBakaiPayService _bakaiPayService;
    private readonly IMapper _mapper;

    public BakaiPayController(ILogger<BakaiPayController> logger, IBakaiPayService bakaiPayService, IMapper mapper)
    {
        _logger = logger;
        _bakaiPayService = bakaiPayService;
        _mapper = mapper;
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
            // Шаг 1: Создать транзакцию через Mediator
            var createResult = await Mediator.Send(new CreateTranBakaiPayReqCommand
            {
                PaymentCode = createReq.PaymentCode,
                PhoneNumber = createReq.PhoneNumber,
                Amount = createReq.Amount,
                OrderId = createReq.OrderId
            });

            if (!createResult.Succeeded)
            {
                _logger.LogError("Failed to create transaction: {Error}", string.Join(", ", createResult.Errors));
                return BadRequest("Failed to create transaction");
            }

            // Шаг 2: Выполнить оплату
            var result = await _bakaiPayService.PayCreate(createReq);

            // Шаг 3: Сохранить результат транзакции
            var resCommand = new CreateTranBakaiPayResCommand
            {
                Status = result.Status,
                OrderId = result.OrderId,
                CreateId = result.Id,
                PaymentCode = result.PaymentCode,
            };

            var saveResult = await Mediator.Send(resCommand);

            if (!saveResult.Succeeded)
            {
                _logger.LogError("Failed to save transaction result: {Error}", string.Join(", ", saveResult.Errors));
                return StatusCode(500, "Failed to save transaction result");
            }

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
            // Шаг 1: Сохранить подтверждение транзакции через Mediator
            var confirmCommandResult = await Mediator.Send(new ConfirmTranBakaiPayReqCommand
            {
                CreateTranId = confirmReq.Id,
                OTPCode = confirmReq.Otp
            });

            if (!confirmCommandResult.Succeeded)
            {
                _logger.LogError("Failed to save confirmation transaction: {Error}", string.Join(", ", confirmCommandResult.Errors));
                return BadRequest("Failed to save confirmation transaction");
            }

            // Шаг 2: Выполнить подтверждение платежа
            var result = await _bakaiPayService.PayConfirm(confirmReq);

            // Шаг 3: Сохранить результат подтверждения транзакции
            var resCommand = new ConfirmTranBakaiPayResCommand
            {
                CreateTranId = confirmReq.Id,
                Status = result.Status,
                OrderId = result.OrderId
            };

            var saveResult = await Mediator.Send(resCommand);

            if (!saveResult.Succeeded)
            {
                _logger.LogError("Failed to save confirmation result: {Error}", string.Join(", ", saveResult.Errors));
                return StatusCode(500, "Failed to save confirmation result");
            }

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
            // Шаг 1: Проверить статус платежа через сервис BakaiPay
            var result = await _bakaiPayService.CheckStatusPay(payId);

            // Шаг 2: Сохранить результат проверки статуса платежа через Mediator
            var saveStatusCommand = new CreateCheckStatusBakaiPayResCommand
            {
                CreateTranId = result.Id,
                PaymentCode = result.PaymentCode,
                Status = result.Status,
                OrderId = result.OrderId,
                ConfirmedAt = Convert.ToDateTime(result.ConfirmedAt),
                ErrMsg = result.ErrMsg
            };

            var saveStatusResult = await Mediator.Send(saveStatusCommand);

            if (!saveStatusResult.Succeeded)
            {
                _logger.LogError("Failed to save check status result: {Error}", string.Join(", ", saveStatusResult.Errors));
                return StatusCode(500, "Failed to save check status result");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing CheckStatusPay for PayId: {PayId}", payId);
            return StatusCode(500, ex.Message);
        }
    }
}
