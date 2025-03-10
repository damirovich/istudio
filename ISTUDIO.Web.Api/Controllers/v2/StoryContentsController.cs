using ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;
using ISTUDIO.Application.Features.StoryContents.Commands.DeleteStoryContent;
using ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;
using ISTUDIO.Application.Features.StoryContents.Queries;
using ISTUDIO.Contracts.Features.StoriesContent;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class StoryContentsController : BaseController2
{
    private readonly IMapper _mapper;

    public StoryContentsController(IMapper mapper, ILogger<StoryContentsController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Получение контента сторис по StoryId
    /// </summary>
    /// <param name="storyId">Идентификатор сторис</param>
    /// <returns>Контент сторис</returns>
    /// <response code="200">Успешное получение контента сторис</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("story/{storyId:int}")]
    public async Task<ICsmActionResult> GetByStoryId([FromRoute] int storyId)
        => await HandleQuery(new GetStoryContentsByStoryIdQuery { StoryId = storyId });

    /// <summary>
    /// Получение контента сторис по Id
    /// </summary>
    /// <param name="id">Идентификатор контента сторис</param>
    /// <returns>Данные контента сторис</returns>
    /// <response code="200">Успешное получение контента сторис</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("{id:int}")]
    public async Task<ICsmActionResult> GetById([FromRoute] int id)
        => await HandleQuery(new GetStoryContentByIdQuery { Id = id });

    /// <summary>
    /// Создание нового контента сторис
    /// </summary>
    /// <param name="content">Данные для создания контента</param>
    /// <returns>Результат операции создания контента</returns>
    /// <response code="200">Контент сторис успешно создан</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateStoryContentVM content)
    {
        var command = _mapper.Map<CreateStoryContentCommand>(content);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование контента сторис
    /// </summary>
    /// <param name="id">Идентификатор контента</param>
    /// <param name="content">Данные для редактирования контента</param>
    /// <returns>Результат операции редактирования контента</returns>
    /// <response code="200">Контент сторис успешно обновлён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    public async Task<ICsmActionResult> Edit( [FromBody] EditStoryContentVM content)
    {
        var command = _mapper.Map<EditStoryContentCommand>(content);

        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление контента сторис
    /// </summary>
    /// <param name="id">Идентификатор контента сторис</param>
    /// <returns>Результат операции удаления контента</returns>
    /// <response code="200">Контент сторис успешно удалён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete("{id:int}")]
    public async Task<ICsmActionResult> Delete([FromRoute] int id)
        => await HandleCommand(new DeleteStoryContentCommand { Id = id });
}
