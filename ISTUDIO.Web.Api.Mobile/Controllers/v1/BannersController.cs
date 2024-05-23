
using ISTUDIO.Application.Features.Banners.Queries;
namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class BannersController : BaseController
{
    /// <summary>
    /// Получение Баннеров
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBanners()
    {
        try
        {
            var result = await Mediator.Send(new GetBannersListQuery());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
