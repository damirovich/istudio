using ISTUDIO.Application.Features.Stories.Commands.CreateStories;
using ISTUDIO.Application.Features.Stories.Commands.DeleteStories;
using ISTUDIO.Application.Features.Stories.Commands.EditStories;
using ISTUDIO.Application.Features.Stories.Queries;
using ISTUDIO.Contracts.Features.Stories;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class StoriesController : BaseController
{
    private readonly IMapper _mapper;

    public StoriesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка сторис с контентом
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetStoriesWithContent()
    {
        try
        {
            var result = await Mediator.Send(new GetStoriesWithContentQuery());
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение сторис по ID
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetStoryById([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new GetStoryByIdQuery { StoryId = id });

            if (result == null)
            {
                return new CsmActionResult(new CsmReturnStatus(-1, "Сторис не найдена"));
            }

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Создание новой сторис
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateStory([FromBody] CreateStoriesVM story)
    {
        try
        {
            var command = _mapper.Map<CreateStoriesCommand>(story);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Редактирование сторис
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> EditStory([FromBody] EditStoriesVM story)
    {
        try
        {
            var command = _mapper.Map<EditStoriesCommand>(story);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Удаление сторис
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> DeleteStory([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteStoriesCommand { StoriesId = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
