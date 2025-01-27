
using ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;
using ISTUDIO.Application.Features.Cashbacks.Commands.DeleteCashbacks;
using ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;
using ISTUDIO.Application.Features.Cashbacks.Queries;
using ISTUDIO.Contracts.Features.Cashbacks;

namespace ISTUDIO.Web.Api.Controllers.v1;
[ApiVersion("1.0")]
public class CashbacksController : BaseController
{
    private readonly ILogger<CashbacksController> _loger;
    private readonly IMapper _mapper;

    public CashbacksController(ILogger<CashbacksController> loger, IMapper mapper)
        => (_loger, _mapper) = (loger, mapper);


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCashbackList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCashbacksQuery
            {
                Parameters = new PaginatedParameters
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

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //public async Task<ICsmActionResult> GetCashbackById([FromQuery] int id)
    //{
    //    try
    //    {
    //        return new CsmActionResult(await Mediator.Send(new GetCashbackByIdQuery
    //        {
    //            CashbackId = id
    //        }));
    //    }
    //    catch (Exception ex)
    //    {
    //        return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
    //    }
    //}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCashback([FromBody] CreateCashbackVM cashback)
    {
        try
        {
            var command = _mapper.Map<CreateCashbackCommand>(cashback);
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
    public async Task<ICsmActionResult> EditCashback([FromBody] EditCashbackVM cashback)
    {
        try
        {
            var command = _mapper.Map<EditCashbackCommand>(cashback);
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
    public async Task<ICsmActionResult> DeleteCashback([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCashbackCommand { Id = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

}
