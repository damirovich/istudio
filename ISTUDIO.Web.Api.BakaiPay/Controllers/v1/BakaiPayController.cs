
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
            _logger.LogWarning("Получен null запрос в PayCheckProps");
            return BadRequest("Номер телефона не может быть пустым");

        }

        try
        {
            var result = await _bakaiPayService.PayCheckProps(phoneNumber);
            return Ok(new { Success = result });
        }
        catch (BadRequestException ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обработке реквизитов PayCheckProps для номера телефона: {RequestId}", phoneNumber);
            return BadRequest (ex.Message);
        }
        catch (Domain.Models.BakaiPay.NotFoundException ex)
        {
            _logger.LogError(ex, "NotFoundException произошло при обработке реквизитов PayCheckProps для номера телефона: {PhoneNumber}", phoneNumber);
            return Ok(new { Success = false });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обработке реквизитов PayCheckProps для номера телефона: {RequestId}", phoneNumber);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> PayCreate([FromBody] BakaiPayCreateOperationReqModel createReq)
    {
        if (createReq == null)
        {
            _logger.LogWarning("Получен null запрос в PayCreate");
            return BadRequest("Данные запроса не может быть пустым");
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
                _logger.LogError("Не удалось добавить данные запроса в базу CreateTranBakaiPayReqCommand: {Error}", string.Join(", ", createResult.Errors));
                return StatusCode(500, "Ошибка при сохранении данных запроса в базу");
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
                _logger.LogError("Не удалось добавить данные ответа в базу  CreateTranBakaiPayResCommand: {Error}", string.Join(", ", saveResult.Errors));
                return StatusCode(500, "Ошибка при сохранении данные ответа в базу");
            }

            return Ok(result);
        }
        catch (BadRequestExceptionBakai ex)
        {
            // Возвращаем 400 Bad Request
            return BadRequest(new { Error = ex.Message });
        }
        catch (UnprocessableEntityException ex)
        {
            // Возвращаем 422 Unprocessable Entity
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (ApiException ex)
        {
            // Для других исключений используем код, указанный в ApiException
            return StatusCode(ex.StatusCode, new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обработке PayCreate для запроса: {Request}", createReq);
            // Для неожиданных исключений возвращаем 500
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Произошла непредвиденная ошибка." });
        }
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> PayConfirm([FromBody] BakaiPayConfirmOperReqModel confirmReq)
    {
        if (confirmReq == null)
        {
            _logger.LogWarning("Получен null запрос в PayConfirm");
            return BadRequest("Данные запрос в PayConfirm не может быть пустым");
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
                _logger.LogError("Не удалось добавить данные запроса в базу ConfirmTranBakaiPayReqCommand {Error}", string.Join(", ", confirmCommandResult.Errors));
                return BadRequest("Не удалось добавить данные запросы в базу");
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
                _logger.LogError("Не удалось добавить данные Ответа в базу ConfirmTranBakaiPayResCommand: {Error}", string.Join(", ", saveResult.Errors));
                return StatusCode(500, "Ошибка при сохранении данные ответа в базу");
            }
           return Ok(result);
        }
        catch (BadRequestExceptionBakai ex)
        {
            // Возвращаем 400 Bad Request
            return BadRequest(new { Error = ex.Message });
        }
        catch (UnprocessableEntityException ex)
        {
            // Возвращаем 422 Unprocessable Entity
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (ApiException ex)
        {
            // Для других исключений используем код, указанный в ApiException
            return StatusCode(ex.StatusCode, new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            // Для неожиданных исключений возвращаем 500
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An unexpected error occurred." });
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
                _logger.LogError("Не удалось добавить данные Ответа в базу CreateCheckStatusBakaiPayResCommand: {Error}", string.Join(", ", saveStatusResult.Errors));
                return StatusCode(500, "Ошибка при сохранении ответа в базу");
            }

            return Ok(result);
        }
        catch (UnprocessableEntityException ex)
        {
            _logger.LogWarning(ex, "Произошла ошибка проверки PayId: {PayId}", payId);
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "Произошла ошибка неверного запросаr PayId: {PayId}", payId);
            return BadRequest(new { Error = ex.Message });
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API error occurred for PayId: {PayId}", payId);
            return StatusCode(ex.StatusCode, new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла непредвиденная ошибка PayId: {PayId}", payId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Произошла непредвиденная ошибка. Повторите попытку позже." });
        }
    }
}
