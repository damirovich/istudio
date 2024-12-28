
using FluentValidation;
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
    /// Получение списка продуктов всех продуктов
    /// </summary>
    /// <param name="pageNumber">Номер страницы (начиная с 0).</param>
    /// <param name="pageSize">Размер страницы (количество элементов на странице).</param>
    /// <returns>Возвращает список продуктов.</returns>
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
        catch (ValidationException ex) // Обработка ошибок FluentValidation
        {
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Метод поиска продуктов с поддержкой пагинации.
    /// </summary>
    /// <param name="page">Параметры пагинации: номер страницы и размер страницы.</param>
    /// <param name="searchTerm">Термин для поиска продуктов.</param>
    /// <returns>Возвращает список продуктов, соответствующих критериям поиска.</returns>
    /// <response code="200">Список продуктов успешно получен.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        catch (ValidationException ex) // Обработка ошибок FluentValidation
        {
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение новинок продуктов за последнюю неделю.
    /// </summary>
    /// <returns>Возвращает список новинок продуктов за последнюю неделю.</returns>
    /// <response code="200">Список новинок успешно получен.</response>
    /// <response code="500">Ошибка сервера.</response>
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
    /// Получение списка продуктов по категории.
    /// </summary>
    /// <param name="page">Параметры пагинации: номер страницы и размер страницы.</param>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <returns>Возвращает список продуктов, соответствующих указанной категории.</returns>
    /// <response code="200">Список продуктов успешно получен.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        catch (ValidationException ex) // Обработка ошибок валидации
        {
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение списка продуктов по магазину.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     GET /api/v1/Products/GetProductsByMagazine?magazineId=1&pageNumber=1&pageSize=10
    /// 
    /// </remarks>
    /// <param name="page">Параметры пагинации: номер страницы и размер страницы.</param>
    /// <param name="magazineId">Идентификатор магазина.</param>
    /// <returns>Возвращает список продуктов, принадлежащих указанному магазину.</returns>
    /// <response code="200">Список продуктов успешно получен.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    /// Получение списка продуктов по подкатегории без пагинации.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     GET /api/v1/Products/GetProductsBySubCategory?categoryId=5
    /// 
    /// </remarks>
    /// <param name="categoryId">Идентификатор подкатегории.</param>
    /// <returns>Возвращает список продуктов, принадлежащих указанной подкатегории.</returns>
    /// <response code="200">Список продуктов успешно получен.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsBySubCategory([FromQuery] int categoryId)
    {
        try
        {

            if (categoryId <= 0)
            {
                return BadRequest(new { Message = "Неверный categoryId. Он должен быть больше 0." });
            }

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
    /// Получение данных продукта по идентификатору.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     GET /api/v1/Products/GetProductsById?productId=10
    /// 
    /// </remarks>
    /// <param name="productId">Идентификатор продукта.</param>
    /// <returns>Возвращает данные продукта с указанным идентификатором.</returns>
    /// <response code="200">Данные продукта успешно получены.</response>
    /// <response code="400">Неверные параметры запроса.</response>
    /// <response code="404">Продукт с указанным идентификатором не найден.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsById([FromQuery] int productId)
    {
        try
        {
            if (productId <= 0)
            {
                return BadRequest(new { Message = "Неверный productId. Он должен быть больше 0." });
            }

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
