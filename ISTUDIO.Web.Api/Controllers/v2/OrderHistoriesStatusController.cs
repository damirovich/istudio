using ISTUDIO.Application.Features.OrderHistories.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для получения данных о статусе заказов
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class OrderHistoriesStatusController : BaseController2
{

    private ILogger<OrderHistoriesStatusController> _logger;

    public OrderHistoriesStatusController(ILogger<OrderHistoriesStatusController> logger) : base(logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Получение данных о статусе заказа по OrderId
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Данные о статусе заказа</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="404">Заказ не найден</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetOrders([FromQuery] int orderId)
    {
        return await HandleQuery(new GetOrderHistoriesById { Id = orderId });
    }
}
