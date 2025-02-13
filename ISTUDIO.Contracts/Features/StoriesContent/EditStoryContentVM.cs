using ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;

namespace ISTUDIO.Contracts.Features.StoriesContent;

public class EditStoryContentVM : IMapWith<EditStoryContentCommand>
{
    public int Id { get; set; }
    public string? MediaDataBase64 { get; set; } // Новое медиа в формате base64 (если нужно обновить)
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoryContentVM, EditStoryContentCommand>()
            .ForMember(dest => dest.MediaData, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MediaDataBase64) ? null : Convert.FromBase64String(src.MediaDataBase64)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue));
    }
}
