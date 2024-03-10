
using Asp.Versioning;
using AutoMapper;
using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.Queries;
using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Contracts.Features.Categories;
using ISTUDIO.Contracts.Features.UserManagement;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Controllers.v1;

//[Authorize]
[ApiVersion("1.0")]
public class CategoriesController : BaseController
{
    private readonly IMapper _mapper;
    public CategoriesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение список всех пользователей системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCategoriesList()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCategoriesListQuery()));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Добавление категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ICsmActionResult> CreateCategories([FromBody] CreateCategoriesVM category)
    {
        try
        {
            var command = _mapper.Map<CreateCategoriesCommand>(category);

            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
