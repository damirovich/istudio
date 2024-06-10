namespace ISTUDIO.Domain.EntityModel;

public class CustomerImagesEntity
{
    // Уникальный идентификатор изображения продукта
    public int Id { get; set; }

    // Тип изображения (например, основное, дополнительное и т. д.)
    public string? TypeImg { get; set; }

    // Ссылка на изображение
    public string? Url { get; set; }

    // Наименование изображения (если применимо)
    public string? Name { get; set; }
    public string? UserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    //Идентификатор Customer который принадлежит изображение
    public int? CustomerId { get; set; }
    public CustomersEntity? Customers { get; set; }  
}
