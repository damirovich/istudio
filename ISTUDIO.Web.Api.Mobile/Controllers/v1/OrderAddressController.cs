

using ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;
using ISTUDIO.Application.Features.OrderAddress.Queries;
using ISTUDIO.Contracts.Features.OrderAddress;
using MediatR;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;
[ApiVersion("1.0")]
public class OrderAddressController : BaseController
{
    private readonly IMapper _mapper;

    public OrderAddressController(IMapper mapper)
    {
        _mapper = mapper;
    }

    

    /// <summary>
    /// Получение данных адреса заказа по Id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderAddressById(int id)
    {
        try
        {
           var result =  await Mediator.Send(new GetOrderAddressByIdQuery { Id = id });
            
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение списка адресов заказов по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderAddressesByUserId(string userId)
    {
        try
        {
            var query = new GetOrderAddressByUserIdQuery { UserId = userId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Добавление адреса заказа
    /// </summary>
    /// <param name="orderAddress"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateOrderAddress([FromBody] CreateOrderAddressVM orderAddress)
    {
        try
        {
            var command = _mapper.Map<CreateOrderAddressCommand>(orderAddress);
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

    /// <summary>
    /// Редактирование данных адреса заказа
    /// </summary>
    /// <param name="orderAddress"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> EditOrderAddress([FromBody] EditOrderAddressVM orderAddress)
    {
        try
        {
            var command = _mapper.Map<EditOrderAddressCommand>(orderAddress);
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
