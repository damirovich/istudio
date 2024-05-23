using ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.DeleteShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Queries;
using ISTUDIO.Contracts.Features.ShoppingsCarts;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class ShopingCartsController : BaseController
{
    private readonly IMapper _mapper;
    public ShopingCartsController(IMapper mapper)
        => _mapper = mapper;
  

    /// <summary>
    /// Получение список продуктов в корзине по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetShoppingCartsByUserId([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetShoppingCartsByUserId
            {
                UserId = userId
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
    public async Task<ICsmActionResult> AddProductToCarts([FromBody] AddProductCartsVM carts)
    {
        try
        {
            var command = _mapper.Map<AddProductToCartsCommand>(carts);

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
    public async Task<ICsmActionResult> ChangeQuantyProductCart([FromBody] ChangeQuantyProductCartVM changeQuanty)
    {
        try
        {
            var command = _mapper.Map<ChangeQuantyProductCartCommand>(changeQuanty);

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


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeletePoductCart([FromQuery] int cartId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteProductToCartCommand { CartId = cartId });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> ClearProductCart([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new ClearShoppingCartsCommand { UserId = userId });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}

