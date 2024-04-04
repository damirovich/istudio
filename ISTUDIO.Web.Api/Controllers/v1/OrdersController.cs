using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Commands.DeleteOrders;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Contracts.Features.Orders;

namespace ISTUDIO.Web.Api.Controllers.v1;

public class OrdersController :BaseController
{
    private readonly IMapper _mapper;

    public OrdersController(IMapper mapper) =>
            _mapper = mapper;


    /// <summary>
    /// Добавление данных заказа
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrders([FromBody] CreateOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<CreateOrdersCommand>(orders);

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
    /// Получение данных о заказе по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrders([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = userId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Получение всех список заказов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrdersList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrdersListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
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
    public async Task<ICsmActionResult> GetOrderDetails([FromQuery] int orderId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderDetailsByIdQuery
            {
                OrderId = orderId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Обновление статуса заказа
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> UpdateStatusOrders([FromBody] EditOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<UpdateStatusOrdersCommand>(orders);

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
    /// Удаление данных заказа
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrdes([FromQuery] int orderId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteOrdersCommand { OrderId = orderId });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

}
