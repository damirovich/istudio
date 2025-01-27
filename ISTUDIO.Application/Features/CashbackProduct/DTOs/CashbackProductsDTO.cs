using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.CashbackProduct.DTOs;

public class CashbackProductsDTO : IMapWith<ProductCashbackEntity>
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Связь с продуктом
    public decimal MaxBonusPercent { get; set; } // Максимальный процент использования бонусов для продукта

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCashbackEntity, CashbackProductsDTO>();
    }
}
