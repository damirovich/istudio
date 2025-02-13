using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Stories.DTOs;

public class StoryContentResDTO : IMapWith<StoryContentEntity>
{
    public string MediaUrl { get; set; } // URL контента (фото/видео)
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; } // Очередность показа

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoryContentEntity, StoryContentResDTO>();
    }
}
