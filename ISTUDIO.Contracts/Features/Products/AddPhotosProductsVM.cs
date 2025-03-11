namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для добавления фото к продукту.
/// </summary>
public class AddPhotosProductsVM
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Фото продукта в формате Base64.
    /// </summary>
    public string ProductPhotos { get; set; }
}