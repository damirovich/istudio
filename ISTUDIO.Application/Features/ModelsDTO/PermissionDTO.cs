using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ModelsDTO;

public class PermissionDTO : IMapWith<PermissionEntity>
{
    public string Name { get; set; } // Например, "CanEditProducts"

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PermissionEntity, PermissionDTO>();
    }
}
