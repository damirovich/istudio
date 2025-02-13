using ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;

namespace ISTUDIO.Contracts.Features.StoriesContent;

public class CreateStoryContentVM : IMapWith<CreateStoryContentCommand>
{
    public int StoryId { get; set; }
    public string MediaDataBase64 { get; set; } // Медиа в формате base64
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStoryContentVM, CreateStoryContentCommand>()
            .ForMember(dest => dest.MediaData, opt => opt.MapFrom(src => Convert.FromBase64String(src.MediaDataBase64)))
            .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue));
    }
}
