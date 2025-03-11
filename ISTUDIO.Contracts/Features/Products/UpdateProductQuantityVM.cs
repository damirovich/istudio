using ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;

namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для обновления количества товара на складе.
/// </summary>
public class UpdateProductQuantityVM : IMapWith<UpdateProductQuantityCommand>
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Новое количество продукта на складе.
    /// </summary>
    public int ProductQuantity { get; set; }

    /// <summary>
    /// Конфигурация маппинга между UpdateProductQuantityVM и UpdateProductQuantityCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductQuantityVM, UpdateProductQuantityCommand>();
    }
}