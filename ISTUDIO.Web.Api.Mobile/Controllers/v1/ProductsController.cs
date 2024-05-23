using ISTUDIO.Application.Features.Products.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class ProductsController : BaseController
{

    /// <summary>
    /// Получение списка всех продуктов
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProducts([FromQuery] PaginatedListVM page)
    {
        try
        {
            var result = await Mediator.Send(new GetProductsListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Метод поиска по продуктам
    /// </summary>
    /// <param name="page"></param>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSearchProducts([FromQuery] PaginatedListVM page, string searchTerm)
    {
        try
        {
            var result = await Mediator.Send(new GetSearchProductsQuery
            {
                Parameters = new PaginationWithSearchParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize,
                    SearchTerm = searchTerm
                }
            });

            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение новинок продуктов за последнию неделю
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWeeklyNewProducts()
    {
        try
        {
            var result = await Mediator.Send(new GetWeeklyNewProductsQuery());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение списка продуктов по категории
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] PaginatedListVM page, int categoryId)
    {
        try
        {
            var result = await Mediator.Send(new GetProductByCategoryIdQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                },
                CategoryId = categoryId
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
