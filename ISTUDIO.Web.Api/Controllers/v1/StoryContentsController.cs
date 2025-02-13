using ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;
using ISTUDIO.Application.Features.StoryContents.Commands.DeleteStoryContent;
using ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;
using ISTUDIO.Application.Features.StoryContents.Queries;
using ISTUDIO.Contracts.Features.StoriesContent;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class StoryContentsController : BaseController
{
    private readonly IMapper _mapper;

    public StoryContentsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение контента сторис по StoryId
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> GetStoryContents([FromQuery] int storyId)
    {
        try
        {
            var result = await Mediator.Send(new GetStoryContentsByStoryIdQuery { StoryId = storyId });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение по Id
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> GetStoryContentsById([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new GetStoryContentByIdQuery { Id = Id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Создание контента для сторис
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> CreateStoryContent([FromBody] CreateStoryContentVM content)
    {
        try
        {
            var command = _mapper.Map<CreateStoryContentCommand>(content);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Редактирование контента сторис
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> EditStoryContent([FromBody] EditStoryContentVM content)
    {
        try
        {
            var command = _mapper.Map<EditStoryContentCommand>(content);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Удаление контента сторис
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICsmActionResult> DeleteStoryContent([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteStoryContentCommand { Id = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
