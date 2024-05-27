using ISTUDIO.Application.Features.FavoriteProducts.Commands.DeleteFavoriteProducts;
using ISTUDIO.Application.Features.FavoriteProducts.Commands.CreateFavoriteProducts;
using ISTUDIO.Application.Features.FavoriteProducts.Queries;
using ISTUDIO.Contracts.Features.FavoriteProducts;

namespace ISTUDIO.Web.Api.Controllers.v1;

public class FavoriteProductsController : BaseController
{

    private readonly IMapper _mapper;

    public FavoriteProductsController(IMapper mapper) =>
            _mapper = mapper;
    /// <summary>
    /// Получение список продуктов в избранном
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetFavoriteProducts([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetFavoriteProductsByUserIdQuery
            {
               UserId = userId
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Добавление продуктов в избранные пользователя
    /// </summary>
    /// <param name="favoriteProducts"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateFavoriteProducts([FromBody] CreateFavoriteProductsVM favoriteProducts)
    {
        try
        {
            var command = _mapper.Map<CreateFavoriteProductsCommand>(favoriteProducts);

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
    /// Удаление продуктов в избранном
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteFavoriteProducts([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteFavoriteProductsCommand { Id = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
