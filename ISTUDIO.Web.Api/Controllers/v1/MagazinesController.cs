
using ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;
using ISTUDIO.Application.Features.Magazines.Commands.DeleteMagazines;
using ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;
using ISTUDIO.Application.Features.Magazines.Queries;
using ISTUDIO.Contracts.Features.Magazines;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class MagazinesController : BaseController
{
    private readonly IMapper _mapper;

    public MagazinesController(IMapper mapper)
    {
        _mapper = mapper;
    }


    /// <summary>
    /// Получение списка магазинов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetMagazinesList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetMagazineListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize,
                }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetMagazineById([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new GetMagazineByIdQuery { MagazineId = id });

            if (result == null)
            {
                return new CsmActionResult(new CsmReturnStatus(-1, "Магазин не найден"));
            }

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Добавление магазина
    /// </summary>
    /// <param name="magazine"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateMagazine([FromBody] CreateMagazineVM magazine)
    {
        try
        {
            var command = _mapper.Map<CreateMagazinesCommand>(magazine);
            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Редактирование данных магазина
    /// </summary>
    /// <param name="magazine"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditMagazine([FromBody] EditMagazineVM magazine)
    {
        try
        {
            var command = _mapper.Map<EditMagazinesCommand>(magazine);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Удаление данные магазина
    /// </summary>
    /// <param name="magazineId"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteMagazine([FromQuery] int magazineId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteMagazinesCommand { MagazineId = magazineId });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

}
