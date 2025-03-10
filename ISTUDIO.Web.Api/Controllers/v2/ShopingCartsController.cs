using ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.DeleteShoppingCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;
using ISTUDIO.Application.Features.ShoppingCarts.Queries;
using ISTUDIO.Contracts.Features.ShoppingsCarts;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class ShoppingCartsController : BaseController2
{
    private readonly IMapper _mapper;

    public ShoppingCartsController(IMapper mapper, ILogger<ShoppingCartsController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Получение списка продуктов в корзине по UserId
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список продуктов в корзине</returns>
    /// <response code="200">Успешное получение списка продуктов</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("user/{userId}")]
    public async Task<ICsmActionResult> GetByUserId([FromRoute] string userId)
        => await HandleQuery(new GetShoppingCartsByUserIdQuery { UserId = userId });

    /// <summary>
    /// Получение актуального списка продуктов в корзине с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список актуальных продуктов в корзине</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    public async Task<ICsmActionResult> GetActual([FromQuery] PaginatedListVM page)
        => await HandleQuery(new GetActualShopCartsQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });

    /// <summary>
    /// Добавление продукта в корзину
    /// </summary>
    /// <param name="carts">Данные продукта для добавления</param>
    /// <returns>Результат операции добавления</returns>
    /// <response code="200">Продукт успешно добавлен</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    public async Task<ICsmActionResult> AddProduct([FromBody] AddProductCartsVM carts)
    {
        var command = _mapper.Map<AddProductToCartsCommand>(carts);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Изменение количества продукта в корзине
    /// </summary>
    /// <param name="changeQuanty">Данные для изменения количества</param>
    /// <returns>Результат изменения количества</returns>
    /// <response code="200">Количество успешно изменено</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut("quantity")]
    public async Task<ICsmActionResult> ChangeQuantity([FromBody] ChangeQuantyProductCartVM changeQuanty)
    {
        var command = _mapper.Map<ChangeQuantyProductCartCommand>(changeQuanty);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление продукта из корзины
    /// </summary>
    /// <param name="cartId">Идентификатор записи корзины</param>
    /// <returns>Результат удаления продукта</returns>
    /// <response code="200">Продукт успешно удалён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete("{cartId:int}")]
    public async Task<ICsmActionResult> DeleteProduct([FromRoute] int cartId)
        => await HandleCommand(new DeleteProductToCartCommand { CartId = cartId });

    /// <summary>
    /// Очистка корзины пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Результат очистки корзины</returns>
    /// <response code="200">Корзина успешно очищена</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete("clear/{userId}")]
    public async Task<ICsmActionResult> ClearCart([FromRoute] string userId)
        => await HandleCommand(new ClearShoppingCartsCommand { UserId = userId });
}
