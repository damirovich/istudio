using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Stories.Commands.CreateStories;

public class CreateStoriesCommand : IRequest<Result>
{
    public byte[] IconPhoto { get; set; } // Иконка как байтовый массив
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
}
