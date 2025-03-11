using ISTUDIO.Application.Features.Stories.Commands.CreateStories;

namespace ISTUDIO.Contracts.Features.Stories;

/// <summary>
/// Модель для создания сторис.
/// </summary>
public class CreateStoriesVM : IMapWith<CreateStoriesCommand>
{
    /// <summary>
    /// Иконка сторис в формате Base64.
    /// </summary>
    public string IconPhotoBase64 { get; set; }

    /// <summary>
    /// Флаг активности сторис.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Дата и время создания сторис.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата и время истечения срока действия сторис.
    /// </summary>
    public DateTime ExpireAt { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateStoriesVM и CreateStoriesCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStoriesVM, CreateStoriesCommand>()
              .ForMember(dest => dest.IconPhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.IconPhotoBase64)))
              .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
              .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt));
    }
}