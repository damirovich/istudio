using ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;

namespace ISTUDIO.Contracts.Features.Magazines;

public class CreateMagazineVM : IMapWith<CreateMagazinesCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoLogoBase64 { get; set; }
    public string UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMagazineVM, CreateMagazinesCommand>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoURL, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoLogoBase64)))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
