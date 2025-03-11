using ISTUDIO.Application.Features.StoryContents.Commands.EditStoryContent;

namespace ISTUDIO.Contracts.Features.StoriesContent;

/// <summary>
/// Модель для редактирования контента сторис.
/// </summary>
public class EditStoryContentVM : IMapWith<EditStoryContentCommand>
{
    /// <summary>
    /// Уникальный идентификатор контента сторис.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Новое медиа-файл в формате Base64 (опционально, если нужно обновить).
    /// </summary>
    public string? MediaDataBase64 { get; set; }

    /// <summary>
    /// Тип контента: "image" (изображение) или "video" (видео).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Порядковый номер контента в сторис.
    /// </summary>
    public int Queue { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditStoryContentVM и EditStoryContentCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoryContentVM, EditStoryContentCommand>()
            .ForMember(dest => dest.MediaData, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.MediaDataBase64) ? null : Convert.FromBase64String(src.MediaDataBase64)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue));
    }
}
