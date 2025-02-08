using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Stories.Commands;

public class CreateStoriesCommand : IMapWith<StoriesEntity>
{
    public string IconUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
}

