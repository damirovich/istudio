using ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;
using ISTUDIO.Contracts.Features.SubCategories;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class SubCategoriesController : BaseController
{
    private readonly IMapper _mapper;
    public SubCategoriesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Добавление под категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateSubCategories([FromBody] CreateSubCategoriesVM subCategory)
    {
        try
        {           
            var command = _mapper.Map<CreateSubCategoriesCommand>(subCategory);

            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
           
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Редактирование данных под категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditSubCategories([FromBody] EditSubCategoriesVM subCategory)
    {
        try
        {
            var command = _mapper.Map<EditSubCategoriesCommand>(subCategory);

            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
   
}
