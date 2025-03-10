
using ISTUDIO.Application.Features.FavoriteProducts.Commands.CreateFavoriteProducts;
using ISTUDIO.Application.Features.FavoriteProducts.Commands.DeleteFavoriteProducts;
using ISTUDIO.Application.Features.FavoriteProducts.Queries;
using ISTUDIO.Contracts.Features.FavoriteProducts;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления избранными продуктами
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class FavoriteProductsController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly ILogger<FavoriteProductsController> _logger;
    public FavoriteProductsController(IMapper mapper, ILogger<FavoriteProductsController> logger) : base(logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Получение списка избранных продуктов пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список избранных продуктов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetFavoriteProducts([FromQuery] string userId)
    {
        return await HandleQuery(new GetFavoriteProductsByUserIdQuery
        {
            UserId = userId
        });
    }

    /// <summary>
    /// Добавление продукта в избранные
    /// </summary>
    /// <param name="favoriteProducts">Данные о продукте</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> CreateFavoriteProducts([FromBody] CreateFavoriteProductsVM favoriteProducts)
    {
        var command = _mapper.Map<CreateFavoriteProductsCommand>(favoriteProducts);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление продукта из избранного
    /// </summary>
    /// <param name="Id">Идентификатор продукта в избранном</param>
    /// <returns>Результат операции</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteFavoriteProducts([FromQuery] int Id)
    {
        return await HandleCommand(new DeleteFavoriteProductsCommand { Id = Id });
    }
}
