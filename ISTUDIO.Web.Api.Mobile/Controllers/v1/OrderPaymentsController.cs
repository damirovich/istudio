using ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Queries;
using ISTUDIO.Contracts.Features.OrderPayments;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class OrderPaymentsController : BaseController
{
    private readonly ILogger<OrderPaymentsController> _logger;
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;
    public OrderPaymentsController(ILogger<OrderPaymentsController> logger, IMapper mapper, IAppDbContext appDbContext)
        => (_logger, _mapper, _appDbContext) = (logger, mapper, appDbContext);


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
    public async Task<IActionResult> CreatePayment([FromBody] CreateOrderPaymentVM payment)
    {
        try
        {
            var status = await _appDbContext.OrderStatus.FirstOrDefaultAsync(x => x.Id == 13) ?? throw new NotFoundException("Статус заказа не найден");
            var command = _mapper.Map<CreateOrderPaymentCommands>(payment);
            command.StatusPayment = status.NameEng;

            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return CreatedAtAction(nameof(GetPaymentById), new { id = result.Succeeded }, result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании платежа");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Редактирование платежа
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditPayment([FromBody] EditOrderPaymentVM payment)
    {
        try
        {
            var command = _mapper.Map<EditOrderPaymentCommands>(payment);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при редактировании платежа");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Удаление платежа
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePayment(int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteOrderPaymentCommands { Id = id });

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении платежа с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
