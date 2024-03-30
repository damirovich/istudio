using Asp.Versioning;
using AutoMapper;
using ISTUDIO.Application.Common.Models;
using ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;
using ISTUDIO.Application.Features.Discounts.Commands.DeleteDiscounts;
using ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;
using ISTUDIO.Application.Features.Discounts.Queries;
using ISTUDIO.Contracts.Features;
using ISTUDIO.Contracts.Features.Discounts;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Mvc;


namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class DiscountsController : BaseController
{
    private readonly IMapper _mapper;

    public DiscountsController(IMapper mapper)
    {
        _mapper = mapper;
    }
    /// <summary>
    /// Получение списка о скидках
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetDiscounts([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetDiscountListQuery
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

    /// <summary>
    /// Добавление данных о скидках 
    /// </summary>
    /// <param name="discount"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateDiscounts([FromBody] CreateDiscountVM discount)
    {
        try
        {
         
            var command = _mapper.Map<CreateDiscountsCommand>(discount);
           
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
    /// <summary>
    /// Редактирование данных о скидках
    /// </summary>
    /// <param name="discount"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditDiscounts([FromBody] EditDiscountVM discount)
    {
        try
        {
            var command = _mapper.Map<EditDiscountsCommand>(discount);

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


    // <summary>
    /// Удаление данных скидки
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteDiscounts([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteDiscountsCommand { DiscountId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
