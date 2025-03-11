namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для редактирования всех фото продукта.
/// </summary>
public class EditAllPhotosProductVM
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Список новых фото продукта в формате Base64.
    /// </summary>
    public List<string> ProductPhotos { get; set; } = new List<string>();
}