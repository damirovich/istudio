using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Magazines.DTOs;

public class MagazineDTO : IMapWith<MagazineEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoLogoURL { get; set; }
    public string UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MagazineEntity, MagazineDTO>();
    }
}
