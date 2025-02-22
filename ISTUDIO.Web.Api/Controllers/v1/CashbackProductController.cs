
using ISTUDIO.Application.Features.CashbackProduct.Commands;
using ISTUDIO.Application.Features.CashbackProduct.Queries;
using ISTUDIO.Contracts.Features.CashbackProducts;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
[Authorize]
public class CashbackProductController : BaseController
{
    private readonly ILogger<CashbackProductController> _loger;
    private readonly IMapper _mapper;

    public CashbackProductController(ILogger<CashbackProductController> loger, IMapper mapper)
        => (_loger, _mapper) = (loger, mapper);


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCashbackProductsList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCashbackProductsQuery
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
    //public async Task<ICsmActionResult> GetCashbackProductById([FromQuery] int id)
    //{
    //    try
    //    {
    //        return new CsmActionResult(await Mediator.Send(new GetCashbackProductByIdQuery
    //        {
    //            CashbackProductId = id
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
    public async Task<ICsmActionResult> CreateCashbackProduct([FromBody] CreateCashbackProductVM cashbackProduct)
    {
        try
        {
            var command = _mapper.Map<CreateCashbackProductCommand>(cashbackProduct);
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
    public async Task<ICsmActionResult> EditCashbackProduct([FromBody] EditCashbackProductVM cashbackProduct)
    {
        try
        {
            var command = _mapper.Map<EditCashbackProductCommand>(cashbackProduct);
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
    public async Task<ICsmActionResult> DeleteCashbackProduct([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCashbackProductCommand { Id = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
