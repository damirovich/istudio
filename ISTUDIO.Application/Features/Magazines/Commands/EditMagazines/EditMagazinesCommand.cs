using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;

public class EditMagazinesCommand : IRequest<Result>, IMapWith<MagazineEntity>
{
    public int MagazineId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] PhotoLogoURL { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditMagazinesCommand, MagazineEntity>();
    }
}
