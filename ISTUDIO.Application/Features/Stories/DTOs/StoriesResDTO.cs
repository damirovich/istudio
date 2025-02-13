using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Stories.DTOs;

public class StoriesResDTO : IMapWith<StoriesEntity>
{
    public int Id { get; set; }
    public string IconUrl { get; set; } // Иконка сторис (например, логотип магазина)
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoriesEntity, StoriesResDTO>();
    }
}
