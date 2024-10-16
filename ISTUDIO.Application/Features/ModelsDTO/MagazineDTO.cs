using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ModelsDTO;

public class MagazineDTO : IMapWith<MagazineEntity>
{
    public int MagazineId { get; set; }
    public string MagazineName { get; set; }
    public string MagazinePhotoUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MagazineEntity, MagazineDTO>()
            .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MagazineName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MagazinePhotoUrl, opt => opt.MapFrom(src => src.PhotoLogoURL));

        profile.CreateMap<MagazineDTO, MagazineEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MagazineId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MagazineName))
            .ForMember(dest => dest.PhotoLogoURL, opt => opt.MapFrom(src => src.MagazinePhotoUrl));
    }
}
