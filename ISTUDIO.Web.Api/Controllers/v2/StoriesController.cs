using ISTUDIO.Application.Features.Stories.Commands.CreateStories;
using ISTUDIO.Application.Features.Stories.Commands.DeleteStories;
using ISTUDIO.Application.Features.Stories.Commands.EditStories;
using ISTUDIO.Application.Features.Stories.Queries;
using ISTUDIO.Contracts.Features.Stories;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class StoriesController : BaseController2
{
    private readonly IMapper _mapper;

    public StoriesController(IMapper mapper, ILogger<StoriesController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Получение списка сторис с контентом
    /// </summary>
    /// <returns>Список сторис с контентом</returns>
    /// <response code="200">Успешное получение списка сторис</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    public async Task<ICsmActionResult> GetStoriesWithContent()
        => await HandleQuery(new GetStoriesWithContentQuery());

    /// <summary>
    /// Получение сторис по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сторис</param>
    /// <returns>Данные сторис</returns>
    /// <response code="200">Успешное получение сторис</response>
    /// <response code="404">Сторис не найдена</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("{id:int}")]
    public async Task<ICsmActionResult> GetStoryById([FromRoute] int id)
        => await HandleQuery(new GetStoryByIdQuery { StoryId = id });

    /// <summary>
    /// Создание новой сторис
    /// </summary>
    /// <param name="story">Данные для создания сторис</param>
    /// <returns>Результат операции создания сторис</returns>
    /// <response code="200">Сторис успешно создана</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateStoriesVM story)
    {
        var command = _mapper.Map<CreateStoriesCommand>(story);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование сторис
    /// </summary>
    /// <param name="id">Идентификатор сторис</param>
    /// <param name="story">Данные для редактирования</param>
    /// <returns>Результат операции редактирования сторис</returns>
    /// <response code="200">Сторис успешно обновлена</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    public async Task<ICsmActionResult> Edit( [FromBody] EditStoriesVM story)
    {
        var command = _mapper.Map<EditStoriesCommand>(story);

        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление сторис
    /// </summary>
    /// <param name="id">Идентификатор сторис</param>
    /// <returns>Результат операции удаления сторис</returns>
    /// <response code="200">Сторис успешно удалена</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete("{id:int}")]
    public async Task<ICsmActionResult> Delete([FromRoute] int id)
        => await HandleCommand(new DeleteStoriesCommand { StoriesId = id });
}
