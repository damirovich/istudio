using ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderUserAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Queries;
using ISTUDIO.Contracts.Features.OrderAddress;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class OrderAddressesController : BaseController
{
    private readonly IMapper _mapper;

    public OrderAddressesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка адресов заказов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderAddressesList()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderAddressesListQuery()));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение данных адреса заказа по Id
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetOrderAddressById([FromQuery] int id)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderAddressByIdQuery { Id = id }));
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
    /// Получение списка адресов заказов по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderAddressesByUserId([FromQuery] string userId)
    {
        try
        {
            var query = new GetOrderAddressByUserIdQuery { UserId = userId };
            var result = await Mediator.Send(query);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Создание адреса заказа пользователя
    /// </summary>
    /// <param name="orderAddress"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrderUserAddress([FromBody] CreateOrderUserAddressVM orderAddress)
    {
        try
        {
            var command = _mapper.Map<CreateOrderUserAddressCommand>(orderAddress);
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
    /// Редактирование данных адреса заказа
    /// </summary>
    /// <param name="orderAddress"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditOrderUserAddress([FromBody] EditOrderUserAddressVM orderAddress)
    {
        try
        {
            var command = _mapper.Map<EditOrderUserAddressCommand>(orderAddress);
            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Удаление данных адреса заказа
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrderUserAddress([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteOrderUserAddressCommand { Id = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
