using ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;
using ISTUDIO.Application.Features.Discounts.Commands.DeleteDiscounts;
using ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;
using ISTUDIO.Application.Features.Discounts.Queries;
using ISTUDIO.Contracts.Features.Discounts;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class DiscountsController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly ILogger<DiscountsController> _logger;  
    public DiscountsController(IMapper mapper, ILogger<DiscountsController> logger) : base (logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Получение списка скидок с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список скидок</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetDiscounts([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetDiscountListQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });
    }

    /// <summary>
    /// Добавление новой скидки
    /// </summary>
    /// <param name="discount">Данные о скидке</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateDiscounts([FromBody] CreateDiscountVM discount)
    {
        var command = _mapper.Map<CreateDiscountsCommand>(discount);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование скидки
    /// </summary>
    /// <param name="discount">Данные о скидке</param>
    /// <returns>Результат операции</returns>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditDiscounts([FromBody] EditDiscountVM discount)
    {
        var command = _mapper.Map<EditDiscountsCommand>(discount);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление скидки по идентификатору
    /// </summary>
    /// <param name="Id">Идентификатор скидки</param>
    /// <returns>Результат операции</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteDiscounts([FromQuery] int Id)
    {
        return await HandleCommand(new DeleteDiscountsCommand { DiscountId = Id });
    }
}
