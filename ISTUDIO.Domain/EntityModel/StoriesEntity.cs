namespace ISTUDIO.Domain.EntityModel;

public class StoriesEntity
{
    public int Id { get; set; }
    public string IconUrl { get; set; } // Иконка сторис (например, логотип магазина)
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }

    public ICollection<StoryContentEntity> StoryContents { get; set; } // Связь с контентом
}
