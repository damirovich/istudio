using ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;
using ISTUDIO.Application.Features.Magazines.DTOs;

namespace ISTUDIO.Contracts.Features.Magazines;

public class EditMagazineVM : IMapWith<EditMagazinesCommand>
{
    [Required(ErrorMessage = "Magazine ID is required.")]
    public int MagazineId { get; set; }

    [Required(ErrorMessage = "Magazine name is required.")]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string PhotoLogoBase64 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditMagazineVM, EditMagazinesCommand>()
               .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.MagazineId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoURL, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoLogoBase64)));

        // Маппинг из MagazineDTO в EditMagazineVM
        profile.CreateMap<MagazinesDTO, EditMagazineVM>()
               .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoBase64, opt => opt.MapFrom(src => src.PhotoLogoURL));
    }
}
