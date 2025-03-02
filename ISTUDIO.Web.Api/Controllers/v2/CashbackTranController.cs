using ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.DeleteCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Queries;
using ISTUDIO.Contracts.Features.CashbackTransaction;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления кэшбэк-транзакциями
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class CashbackTranController : BaseController2
{


    private readonly ILogger<CashbackTranController> _logger;
    private readonly IMapper _mapper;

    public CashbackTranController(ILogger<CashbackTranController> logger, IMapper mapper) : base(logger)
        => (_mapper, _logger) = (mapper, logger);


    /// <summary>
    /// Получение списка кэшбэк-транзакций с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список транзакций</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetTransactionList([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetCashTransactionResQuery()
        {
            Parameters = new PaginatedParameters()
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });
    }

    /// <summary>
    /// Получение информации о конкретной кэшбэк-транзакции по ID
    /// </summary>
    /// <param name="id">Идентификатор транзакции</param>
    /// <returns>Данные о транзакции</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetTransactionById([FromQuery] int id)
    {
        return await HandleQuery(new GetCashbackTranByIdQuery { CashTranId = id });
    }

    /// <summary>
    /// Создание новой кэшбэк-транзакции
    /// </summary>
    /// <param name="transaction">Данные для создания</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Транзакция успешно создана</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateTransaction([FromBody] CreateCashTransactionVM transaction)
    {
        var command = _mapper.Map<CreateCashTransactionCommand>(transaction);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование существующей кэшбэк-транзакции
    /// </summary>
    /// <param name="transaction">Данные для обновления</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Транзакция успешно обновлена</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditTransaction([FromBody] EditCashTransactionVM transaction)
    {
        var command = _mapper.Map<EditCashTransactionCommand>(transaction);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление кэшбэк-транзакции по ID
    /// </summary>
    /// <param name="id">Идентификатор транзакции</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Транзакция успешно удалена</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteTransaction([FromQuery] int id)
    {
        return await HandleCommand(new DeleteCashTransactionCommand { CashTranId = id });
    }


}
