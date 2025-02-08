namespace ISTUDIO.Domain.EntityModel;

public class StoryContentEntity
{
    public int Id { get; set; }
    public int StoryId { get; set; }
    public string MediaUrl { get; set; } // Ссылка на фото/видео
    public string Type { get; set; } // "image" или "video"
    public int Queue { get; set; } // Очередность показа

    public StoriesEntity Story { get; set; }
}
