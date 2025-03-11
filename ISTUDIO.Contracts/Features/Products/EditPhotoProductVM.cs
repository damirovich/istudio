
namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для редактирования фото продукта.
/// </summary>
public class EditPhotoProductVM
{
    /// <summary>
    /// Уникальный идентификатор фото.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фото продукта в формате Base64.
    /// </summary>
    public string ProductPhotos { get; set; }
}