
using AutoMapper;
using ISTUDIO.Application.Common.Exceptions;
using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Contracts.Features.Orders;

using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

public class OrdersController :BaseController
{
    private readonly IMapper _mapper;

    public OrdersController(IMapper mapper) =>
            _mapper = mapper;


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateOrders([FromBody] CreateOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<CreateOrdersCommand>(orders);

            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrders([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = userId
            });

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    /// <summary>
    /// Получение детальную информацию о заказе 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
    {
        try
        {
            var result = await Mediator.Send(new GetOrderDetailsByIdQuery { OrderId = orderId });
            return Ok(result);

        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
