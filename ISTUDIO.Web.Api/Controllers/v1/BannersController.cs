using ISTUDIO.Application.Features.Banners.Commands.CreateBanners;
using ISTUDIO.Application.Features.Banners.Commands.DeleteBannes;
using ISTUDIO.Application.Features.Banners.Commands.EditBanners;
using ISTUDIO.Application.Features.Banners.Queries;
using ISTUDIO.Contracts.Features.Banners;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;
[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class BannersController : BaseController
{
    private readonly IMapper _mapper;

    public BannersController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка баннеров
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetBannersList()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetBannersListQuery()));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение данные баннера 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetBannersById([FromQuery] int id)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetBannersByIdQuery 
            {
                BannerId = id
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Добавление баннера
    /// </summary>
    /// <param name="banner"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateBanner([FromBody] CreateBannerVM banner)
    {
        try
        {
            var command = _mapper.Map<CreateBannersCommand>(banner);
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
    /// Редактирование данных баннера
    /// </summary>
    /// <param name="banner"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditBanner([FromBody] EditBannerVM banner)
    {
        try
        {
            var command = _mapper.Map<EditBannerCommand>(banner);
            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Удаление данных баннера
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteBanner([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteBannerCommand { BannerId = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
