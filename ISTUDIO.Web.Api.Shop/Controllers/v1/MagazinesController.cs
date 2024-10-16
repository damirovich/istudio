using ISTUDIO.Application.Features.Magazines.Queries;

namespace ISTUDIO.Web.Api.Shop.Controllers.v1;

[ApiVersion("1.0")]
public class MagazinesController : BaseController
{
    /// <summary>
    /// Получение списка магазинов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMagazinesList([FromQuery] PaginatedListVM page)
    {
        try
        {
            var magazines = await Mediator.Send(new GetMagazineListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize,
                }
            });

            return Ok(magazines);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение информации о магазине по ID
    /// </summary>
    /// <param name="id">Идентификатор магазина</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMagazineById([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new GetMagazineByIdQuery { MagazineId = id });

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Магазин не найден");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
