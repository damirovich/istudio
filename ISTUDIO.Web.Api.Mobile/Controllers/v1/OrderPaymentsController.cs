using Azure.Core;
using ISTUDIO.Application.Features.OrderPayments.Commands.AddReceptPhotoPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Queries;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Contracts.Features.OrderPayments;
using ISTUDIO.Contracts.Features.Orders;
using ISTUDIO.Domain.Models.BakaiPay;
using ISTUDIO.Web.Api.Mobile.Services.BakaiPayService;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class OrderPaymentsController : BaseController
{
    private readonly ILogger<OrderPaymentsController> _logger;
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;
    private readonly IBakaiPayApiClient _apiClient;
    public OrderPaymentsController(ILogger<OrderPaymentsController> logger, IMapper mapper, IAppDbContext appDbContext, IBakaiPayApiClient apiClient)
        => (_logger, _mapper, _appDbContext, _apiClient) = (logger, mapper, appDbContext, apiClient);


    ///// <summary>
    ///// Получение списка платежей
    ///// </summary>
    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetPaymentList([FromQuery] PaginatedListVM page)
    //{
    //    try
    //    {
    //        var result = await Mediator.Send(new GetOrderPaymentsQuery
    //        {
    //            Parameters = new PaginatedParameters
    //            {
    //                PageNumber = page.PageNumber,
    //                PageSize = page.PageSize
    //            }
    //        });

    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Ошибка при получении списка платежей");
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}

    /// <summary>
    /// Получение платежа по ID
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        try
        {
            var result = await Mediator.Send(new GetOrderPaymentsByIdQuery
            {
                OrderPayId = id
            });

            if (result == null)
                return NotFound($"Платеж с ID {id} не найден.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении платежа с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Создание платежа
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePayment([FromBody, Required] CreateOrderPaymentVM payment)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = await _appDbContext.OrderStatus.FirstOrDefaultAsync(x => x.Id == 10)//В ожидании оплаты
                         ?? throw new Application.Common.Exceptions.NotFoundException("Статус заказа не найден");

            var payOrder = new CreateOrderPaymentCommands()
            {
                UserId = payment.UserId,
                OrderId = payment.OrderId,
                PaymentMethodId = payment.PaymentMethodId,
                Amount = payment.FinalAmount,
                CreditBonusAmount = payment.BonusAmount,
                DebitBonusAmount = payment.EarnedBonusAmount,
                StatusPayment = status.NameEng,
            };

            var payMethod = await _appDbContext.PaymentMethods.FirstOrDefaultAsync(s => s.Id == payment.PaymentMethodId);

            if (payMethod == null)
                return BadRequest("Не было найдено метод оплаты ");

            if (payment.PaymentMethodId == 1) // Если оплата через Бакай Банк
            {
                if (string.IsNullOrWhiteSpace(payment.BakaiPhoneNumber))
                    return BadRequest("Для оплаты через Бакай Банк необходимо указать номер телефона.");

                var requestBakaiPay = new BakaiPayCreateOperationReqModel()
                {
                    PhoneNumber = payment.BakaiPhoneNumber,
                    Amount = Convert.ToInt32(payment.FinalAmount),
                    PaymentCode = payment.OrderId.ToString(),
                    OrderId = payment.OrderId.ToString()
                };

                var result = await Mediator.Send(payOrder);

                if (result.Succeeded)
                {
                    var resPayCreateBakai = await _apiClient.PayCreate(requestBakaiPay);
                    return Ok(resPayCreateBakai);
                }

                return BadRequest(result.Errors);
            }

            // Для других методов оплаты (mBank, Optima Bank, Cash и т.д.)
            var paymentResult = await Mediator.Send(payOrder);

            if (paymentResult.Succeeded)
                return Ok(paymentResult);

            return BadRequest(paymentResult.Errors);
        }
        catch (Application.Common.Exceptions.NotFoundException ex)
        {
            _logger.LogWarning(ex, "Ошибка: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании платежа");
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка.");
        }
    }
    ///// <summary>
    ///// Редактирование платежа
    ///// </summary>
    //[HttpPut]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> EditPayment([FromBody] EditOrderPaymentVM payment)
    //{
    //    try
    //    {
    //        var command = _mapper.Map<EditOrderPaymentCommands>(payment);
    //        var result = await Mediator.Send(command);

    //        if (result.Succeeded)
    //            return Ok(result);

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Ошибка при редактировании платежа");
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}


    /// <summary>
    /// Добавление фотографии квитанции к заказу
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReceiptPhoto([FromBody] AddReceiptPhotoOrderVM model)
    {
        try
        {
            var command = _mapper.Map<AddReceipPhotoOrderPaymentCommand>(model);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    ///// <summary>
    ///// Удаление платежа
    ///// </summary>
    //[HttpDelete("{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> DeletePayment(int id)
    //{
    //    try
    //    {
    //        var result = await Mediator.Send(new DeleteOrderPaymentCommands { Id = id });

    //        if (result.Succeeded)
    //            return Ok(result);

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Ошибка при удалении платежа с ID {Id}", id);
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}
}
