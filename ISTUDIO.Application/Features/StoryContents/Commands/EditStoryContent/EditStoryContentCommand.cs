
namespace ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;

public class EditStoryContentCommand : IRequest<Result>
{
    public int Id { get; set; }
    public byte[]? MediaData { get; set; } // Новое медиа (если нужно обновить)
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; }
}
