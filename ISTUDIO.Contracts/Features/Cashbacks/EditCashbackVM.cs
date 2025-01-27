using ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;

namespace ISTUDIO.Contracts.Features.Cashbacks;

public class EditCashbackVM : IMapWith<EditCashbackCommand>
{
    public int Id { get; set; }
    public decimal CashbackPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashbackVM, EditCashbackCommand>();
    }

}
