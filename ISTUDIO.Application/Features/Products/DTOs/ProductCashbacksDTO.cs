using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.DTOs;

public class ProductCashbacksDTO : IMapWith<CashbackEntity>
{
    public decimal CashbackPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CashbackEntity, ProductCashbacksDTO>();
    }
}
