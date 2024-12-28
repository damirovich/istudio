using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.UpAppInfo.DTOs;

public class InfoUpAppDTO : IMapWith<AppUpdateInfoEntity>
{
    public string LatestVersion { get; set; }
    public bool UpdateRequired { get; set; }
    public string UpdateUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUpdateInfoEntity, InfoUpAppDTO>()
            .ForMember(dest => dest.LatestVersion, opt => opt.MapFrom(src => src.LatestVersion))
            .ForMember(dest => dest.UpdateRequired, opt => opt.MapFrom(src => src.UpdateRequired))
            .ForMember(dest => dest.UpdateUrl, opt => opt.MapFrom(src => src.UpdateUrl));
    }

}
