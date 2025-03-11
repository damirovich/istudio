
using ISTUDIO.Application.Features.Products.Commands.UpdateProductSum;

namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для обновления цены продукта.
/// </summary>
public class UpdateProductSummVM : IMapWith<UpdateProductSummaCommand>
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Новая сумма (цена) продукта.
    /// </summary>
    public decimal ProductSumma { get; set; }

    /// <summary>
    /// Конфигурация маппинга между UpdateProductSummVM и UpdateProductSummaCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductSummVM, UpdateProductSummaCommand>();
    }
}