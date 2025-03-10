using ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Queries;
using ISTUDIO.Contracts.Features.OrderPayments;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления платежами заказов
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class OrderPaymentsController : BaseController2
{
    private readonly ILogger<OrderPaymentsController> _logger;
    private readonly IMapper _mapper;

    public OrderPaymentsController(ILogger<OrderPaymentsController> logger, IMapper mapper) : base (logger)
        => (_logger, _mapper) = (logger, mapper);

    /// <summary>
    /// Получение списка платежей заказов
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список платежей заказов</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderPaymentList([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetOrderPaymentsQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });
    }

    /// <summary>
    /// Получение данных о платеже заказа по ID
    /// </summary>
    /// <param name="id">Идентификатор платежа</param>
    /// <returns>Данные платежа</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("by-id")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderPaymentById([FromQuery] int id)
    {
        return await HandleQuery(new GetOrderPaymentsByIdQuery { OrderPayId = id });
    }

    /// <summary>
    /// Создание нового платежа заказа
    /// </summary>
    /// <param name="orderPayment">Данные для создания</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Платеж успешно создан</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrderPayment([FromBody] CreateOrderPaymentVM orderPayment)
    {
        var command = _mapper.Map<CreateOrderPaymentCommands>(orderPayment);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование платежа заказа
    /// </summary>
    /// <param name="orderPayment">Данные для обновления</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Платеж успешно обновлен</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditOrderPayment([FromBody] EditOrderPaymentVM orderPayment)
    {
        var command = _mapper.Map<EditOrderPaymentCommands>(orderPayment);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление платежа заказа
    /// </summary>
    /// <param name="id">Идентификатор платежа</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Платеж успешно удален</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrderPayment([FromQuery] int id)
    {
        return await HandleCommand(new DeleteOrderPaymentCommands { Id = id });
    }
}
