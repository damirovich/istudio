using ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderUserAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Queries;
using ISTUDIO.Contracts.Features.OrderAddress;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления адресами заказов
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class OrderAddressesController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly ILogger<OrderAddressesController> _logger;
    public OrderAddressesController(IMapper mapper, ILogger<OrderAddressesController> logger) : base(logger)
    {
        _mapper = mapper;
        _logger = logger;
    }
    
    /// <summary>
    /// Получение списка адресов заказов
    /// </summary>
    /// <returns>Список адресов заказов</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderAddressesList()
    {
        return await HandleQuery(new GetOrderAddressesListQuery());
    }

    /// <summary>
    /// Получение данных адреса заказа по ID
    /// </summary>
    /// <param name="id">Идентификатор адреса заказа</param>
    /// <returns>Данные адреса</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="404">Адрес заказа не найден</response>
    [HttpGet("by-id")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetOrderAddressById([FromQuery] int id)
    {
        return await HandleQuery(new GetOrderAddressByIdQuery { Id = id });
    }

    /// <summary>
    /// Получение списка адресов заказов по UserId
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список адресов заказов пользователя</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("by-user")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderAddressesByUserId([FromQuery] string userId)
    {
        return await HandleQuery(new GetOrderAddressByUserIdQuery { UserId = userId });
    }

    /// <summary>
    /// Создание нового адреса заказа
    /// </summary>
    /// <param name="orderAddress">Данные для создания</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Адрес успешно создан</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrderUserAddress([FromBody] CreateOrderUserAddressVM orderAddress)
    {
        var command = _mapper.Map<CreateOrderUserAddressCommand>(orderAddress);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование адреса заказа
    /// </summary>
    /// <param name="orderAddress">Данные для обновления</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Адрес успешно обновлен</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditOrderUserAddress([FromBody] EditOrderUserAddressVM orderAddress)
    {
        var command = _mapper.Map<EditOrderUserAddressCommand>(orderAddress);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление адреса заказа
    /// </summary>
    /// <param name="id">Идентификатор адреса</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Адрес успешно удален</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrderUserAddress([FromQuery] int id)
    {
        return await HandleCommand(new DeleteOrderUserAddressCommand { Id = id });
    }
}
