
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Customers.DTOs;

public class CustomerImagesResponseDTO : IMapWith<CustomerImagesEntity>
{
    public int Id { get; set; }
    public string? PhototUrl { get; set; }
    public string? NameImage { get; set; }
    public string? UserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CustomerImagesEntity, CustomerImagesResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PhototUrl, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.NameImage, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}