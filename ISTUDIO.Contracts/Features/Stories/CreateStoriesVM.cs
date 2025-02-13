using ISTUDIO.Application.Features.Stories.Commands.CreateStories;

namespace ISTUDIO.Contracts.Features.Stories;

public class CreateStoriesVM : IMapWith<CreateStoriesCommand>
{
    public string IconPhotoBase64 { get; set; } // Иконка в base64
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStoriesVM, CreateStoriesCommand>()
                  .ForMember(dest => dest.IconPhoto,
                      opt => opt.MapFrom(src => Convert.FromBase64String(src.IconPhotoBase64)))
                  .ForMember(dest => dest.IsActive,
                      opt => opt.MapFrom(src => src.IsActive))
                  .ForMember(dest => dest.CreatedAt,
                      opt => opt.MapFrom(src => src.CreatedAt))
                  .ForMember(dest => dest.ExpireAt,
                      opt => opt.MapFrom(src => src.ExpireAt));
    }
}
