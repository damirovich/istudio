using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Stories.DTOs;

public class StoriesWithContentResDTO :IMapWith<StoriesEntity>
{
    public string IconUrl { get; set; } // URL иконки
    public List<StoryContentResDTO> Contents { get; set; } // Список контента сторис

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoriesEntity, StoriesWithContentResDTO>()
            .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.IconUrl))
            .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.StoryContents));
    }
}
