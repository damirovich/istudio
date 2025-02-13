
namespace ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;

public class CreateStoryContentCommand : IRequest<Result>
{
    public int StoryId { get; set; }
    public byte[] MediaData { get; set; } // Медиа в виде byte[]
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; }
}