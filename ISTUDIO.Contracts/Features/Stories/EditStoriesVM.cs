using ISTUDIO.Application.Features.Stories.Commands.EditStories;

namespace ISTUDIO.Contracts.Features.Stories;

public class EditStoriesVM : IMapWith<EditStoriesCommand>
{
    public int Id { get; set; }
    public string? IconPhotoBase64 { get; set; } // Новая иконка в base64 (опционально)
    public bool IsActive { get; set; }
    public DateTime ExpireAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoriesVM, EditStoriesCommand>()
            .ForMember(dest => dest.IconPhoto, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.IconPhotoBase64) ? null : Convert.FromBase64String(src.IconPhotoBase64)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => src.ExpireAt));
    }
}
