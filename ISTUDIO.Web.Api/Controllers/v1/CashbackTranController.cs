
using ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.DeleteCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;
using ISTUDIO.Application.Features.CashbackTransactions.Queries;
using ISTUDIO.Contracts.Features.CashbackTransaction;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class CashbackTranController : BaseController    
{
    private readonly ILogger _loger;
    private readonly IMapper _mapper;

    public CashbackTranController(ILogger loger, IMapper mapper)
        => (_mapper, loger) = (mapper, loger);

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetTransactionList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCashTransactionResQuery()
            {
                Parameters = new PaginatedParameters()
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            }));

        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetTransactionById([FromQuery] int id)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCashbackTranByIdQuery
            {
                CashTranId = id
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateTransaction([FromBody] CreateCashTransactionVM transaction)
    {
        try
        {
            var command = _mapper.Map<CreateCashTransactionCommand>(transaction);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditTransaction([FromBody] EditCashTransactionVM transaction)
    {
        try
        {
            var command = _mapper.Map<EditCashTransactionCommand>(transaction);
            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteTransaction([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCashTransactionCommand { CashTranId = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
