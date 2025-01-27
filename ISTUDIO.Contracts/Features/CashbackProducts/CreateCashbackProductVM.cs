using ISTUDIO.Application.Features.CashbackProduct.Commands;

namespace ISTUDIO.Contracts.Features.CashbackProducts;

public class CreateCashbackProductVM : IMapWith<CreateCashbackProductCommand>
{
    public int ProductId { get; set; }
    public decimal MaxBonusPercent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashbackProductVM, CreateCashbackProductCommand>();
    }
}
