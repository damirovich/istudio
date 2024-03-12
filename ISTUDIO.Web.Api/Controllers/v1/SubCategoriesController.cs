
using Asp.Versioning;
using AutoMapper;
using ISTUDIO.Application.Common.Models;
using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.Queries;
using ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.DeleteSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;
using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Contracts.Features.Categories;
using ISTUDIO.Contracts.Features.SubCategories;
using ISTUDIO.Contracts.Features.UserManagement;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Controllers.v1;

//[Authorize]
[ApiVersion("1.0")]
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

            return new CsmActionResult<Result>(await Mediator.Send(command));
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

            return new CsmActionResult<Result>(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Удаление данных Под категории
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteSubCategory([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteSubCategoriesCommand { SubCategoryId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
