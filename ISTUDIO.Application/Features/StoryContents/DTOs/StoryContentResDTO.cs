using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.StoryContents.DTOs;

public class StoryContentResDTO : IMapWith<StoryContentEntity>
{
    public int Id { get; set; }
    public int StoryId { get; set; }
    public string MediaUrl { get; set; } 
    public string Type { get; set; } 
    public int Queue { get; set; } 

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoryContentEntity, StoryContentResDTO>();
    }
}
