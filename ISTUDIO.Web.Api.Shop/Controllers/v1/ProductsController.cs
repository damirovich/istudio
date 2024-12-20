
using ISTUDIO.Application.Features.Products.Queries;
using Swashbuckle.Swagger.Annotations;

namespace ISTUDIO.Web.Api.Shop.Controllers.v1;

/// <summary>
/// Контроллер для работы с продуктами.
/// </summary>

[ApiVersion("1.0")]
public class ProductsController : BaseController
{
    /// <summary>
    /// Получение списка всех продуктов.
    /// </summary>
    /// <param name="page">Объект пагинации, содержащий номер страницы и размер страницы.</param>
    /// <returns>
    /// Возвращает список продуктов, соответствующих переданным параметрам пагинации.
    /// </returns>
    /// <response code="200">Список продуктов успешно получен.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Получение списка продуктов по категории
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProductsByMagazine([FromQuery] PaginatedListVM page, int magazineId)
    {
        try
        {
            var result = await Mediator.Send(new GetProductsByMagazineIdQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                },
                MagazineId = magazineId
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение списка продуктов по категории без пагинации
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProductsBySubCategory([FromQuery] int categoryId)
    {
        try
        {
            var result = await Mediator.Send(new GetProductsBySubCategoryId
            {
                CategoryId = categoryId
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение данные продукта по ProductId
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProductsById([FromQuery] int productId)
    {
        try
        {
            var result = await Mediator.Send(new GetProductsByIdQuery
            {
                ProductId = productId
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
