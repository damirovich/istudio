using ISTUDIO.Application.Features.Stories.Commands.EditStories;

namespace ISTUDIO.Contracts.Features.Stories;

/// <summary>
/// Модель для редактирования сторис.
/// </summary>
public class EditStoriesVM : IMapWith<EditStoriesCommand>
{
    /// <summary>
    /// Уникальный идентификатор сторис.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Новая иконка сторис в формате Base64 (опционально).
    /// </summary>
    public string? IconPhotoBase64 { get; set; }

    /// <summary>
    /// Флаг активности сторис.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Дата и время истечения срока действия сторис.
    /// </summary>
    public DateTime ExpireAt { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditStoriesVM и EditStoriesCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoriesVM, EditStoriesCommand>()
            .ForMember(dest => dest.IconPhoto, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.IconPhotoBase64) ? null : Convert.FromBase64String(src.IconPhotoBase64)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt));
    }
}