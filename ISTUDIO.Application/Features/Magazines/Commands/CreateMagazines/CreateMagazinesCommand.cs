using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;


public class CreateMagazinesCommand : IRequest<Result>, IMapWith<MagazineEntity>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] PhotoLogoURL { get; set; }
    public string UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMagazinesCommand, MagazineEntity>();
    }
}
