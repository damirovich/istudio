using ISTUDIO.Application.Features.StoryContents.Commands.CreateStoryContent;

namespace ISTUDIO.Contracts.Features.StoriesContent;

/// <summary>
/// Модель для создания контента сторис.
/// </summary>
public class CreateStoryContentVM : IMapWith<CreateStoryContentCommand>
{
    /// <summary>
    /// Уникальный идентификатор сторис, к которой относится контент.
    /// </summary>
    public int StoryId { get; set; }

    /// <summary>
    /// Медиа-файл в формате Base64 (изображение или видео).
    /// </summary>
    public string MediaDataBase64 { get; set; }

    /// <summary>
    /// Тип контента: "image" (изображение) или "video" (видео).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Порядковый номер контента в сторис.
    /// </summary>
    public int Queue { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateStoryContentVM и CreateStoryContentCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStoryContentVM, CreateStoryContentCommand>()
            .ForMember(dest => dest.MediaData, opt => opt.MapFrom(src => Convert.FromBase64String(src.MediaDataBase64)))
            .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue));
    }
}