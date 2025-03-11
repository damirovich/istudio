using ISTUDIO.Application.Features.Products.Commands.UpdateProductIsActive;

namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для обновления статуса активности продукта.
/// </summary>
public class UpdateProductActiveVM : IMapWith<UpdateProductActiveCommand>
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Статус активности продукта (true - активен, false - неактивен).
    /// </summary>
    public bool ProductActive { get; set; }

    /// <summary>
    /// Конфигурация маппинга между UpdateProductActiveVM и UpdateProductActiveCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductActiveVM, UpdateProductActiveCommand>();
    }
}