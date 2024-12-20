namespace ISTUDIO.Contracts.Features;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// Параметры пагинации.
/// </summary>
public class PaginatedListVM {
    /// <summary>
    /// Номер страницы (начиная с 0).
    /// </summary>
    [Required(ErrorMessage = "Номер страницы обязателен.")]
    [Range(0, int.MaxValue, ErrorMessage = "Номер страницы должен быть больше или равен 0.")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    [Required(ErrorMessage = "Размер страницы обязателен.")]
    [Range(1, int.MaxValue, ErrorMessage = "Размер страницы должен быть больше 0.")]
    public int PageSize { get; set; }
}
