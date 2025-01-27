using ISTUDIO.Application.Features.CashbackProduct.Commands;

namespace ISTUDIO.Contracts.Features.CashbackProducts;

public class EditCashbackProductVM : IMapWith<EditCashbackProductCommand>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal MaxBonusPercent { get; set; }

    public void Mapping(Profile profile)
    { 
        profile.CreateMap<EditCashbackProductVM, EditCashbackProductCommand>();
    }
}
