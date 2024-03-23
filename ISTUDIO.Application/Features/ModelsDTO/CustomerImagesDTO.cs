using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ModelsDTO;

public class CustomerImagesDTO : IMapWith<CustomerImagesEntity>
{
    public string? TypeImg { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CustomerImagesEntity, CustomerImagesDTO>()
            .ForMember(dest => dest.TypeImg, opt => opt.MapFrom(src => src.TypeImg))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        profile.CreateMap<CustomerImagesDTO, CustomerImagesEntity>()
          .ForMember(dest => dest.TypeImg, opt => opt.MapFrom(src => src.TypeImg))
          .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
