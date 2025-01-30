using ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.DeleteCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Queries;
using ISTUDIO.Contracts.Features.CashbackTransaction;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class CashbackTranController : BaseController
{
    private readonly ILogger<CashbackTranController> _logger;
    private readonly IMapper _mapper;

    public CashbackTranController(ILogger<CashbackTranController> logger, IMapper mapper)
        => (_logger, _mapper) = (logger, mapper);

    /// <summary>
    /// Получение списка транзакций кэшбека
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransactionList([FromQuery] PaginatedListVM page)
    {
        try
        {
            var result = await Mediator.Send(new GetCashTransactionResQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка транзакций кэшбека");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение транзакции кэшбека по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        try
        {
            var result = await Mediator.Send(new GetCashbackTranByIdQuery
            {
                CashTranId = id
            });

            if (result == null)
                return NotFound($"Транзакция с ID {id} не найдена.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении транзакции с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Создание транзакции кэшбека
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateCashTransactionVM transaction)
    {
        try
        {
            var command = _mapper.Map<CreateCashTransactionCommand>(transaction);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return CreatedAtAction(nameof(GetTransactionById), new { id = result.Succeeded }, result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании транзакции кэшбека");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Редактирование транзакции кэшбека
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditTransaction([FromBody] EditCashTransactionVM transaction)
    {
        try
        {
            var command = _mapper.Map<EditCashTransactionCommand>(transaction);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при редактировании транзакции кэшбека");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Удаление транзакции кэшбека
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCashTransactionCommand { CashTranId = id });

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении транзакции с ID {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
