
using ISTUDIO.Application.Features.OrderHistories.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class OrderHistoriesStatusController : BaseController
{
    /// <summary>
    /// Получение данных о статусе заказа по OrderId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetOrders([FromQuery] int orderId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderHistoriesById
            {
                Id = orderId
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
}
