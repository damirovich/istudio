using ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.DeleteShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Queries;
using ISTUDIO.Contracts.Features.ShoppingsCarts;


namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class ShopingCartsController : BaseController
{
    private readonly IMapper _mapper;
    public ShopingCartsController(IMapper mapper)
        => _mapper = mapper;
    /// <summary>
    /// Получени список продуктов в корзине по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetShoppingCartsByUserId([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new GetShoppingCartsByUserId
            {
                UserId = userId
            });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddProductToCarts([FromBody] AddProductCartsVM carts)
    {
        try
        {
            var command = _mapper.Map<AddProductToCartsCommand>(carts);

            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors);

        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (BadRequestException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangeQuantyProductCart([FromBody] ChangeQuantyProductCartVM changeQuanty)
    {
        try
        {
            var command = _mapper.Map<ChangeQuantyProductCartCommand>(changeQuanty);

            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return Ok(result);

            return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (BadRequestException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePoductCart([FromQuery] int cartId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteProductToCartCommand { CartId = cartId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ClearProductCart([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new ClearShoppingCartsCommand { UserId = userId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}

