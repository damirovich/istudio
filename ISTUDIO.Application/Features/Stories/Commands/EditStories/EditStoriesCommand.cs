    namespace ISTUDIO.Application.Features.Stories.Commands.EditStories;

public class EditStoriesCommand : IRequest<Result>
{
    public int Id { get; set; }
    public byte[]? IconPhoto { get; set; } // Новая иконка (если нужно обновить)
    public bool IsActive { get; set; }
    public DateTime ExpireAt { get; set; }
}
