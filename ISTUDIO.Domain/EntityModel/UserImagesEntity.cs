namespace ISTUDIO.Domain.EntityModel;

public class UserImagesEntity
{
    // Уникальный идентификатор изображения продукта
    public int Id { get; set; }

    // Тип изображения (например, основное, дополнительное и т. д.)
    public string? TypeImg { get; set; }

    // Ссылка на изображение
    public string? Url { get; set; }

    // Наименование изображения (если применимо)
    public string? Name { get; set; }

    // Тип содержимого изображения (например, image/jpeg, image/png и т. д.)
    public string? ContentType { get; set; }
    //Идентификатор User который принадлежит изображение

    public string UsersId { get; set; }
}
