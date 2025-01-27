using ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;

namespace ISTUDIO.Contracts.Features.Cashbacks;

public class CreateCashbackVM  : IMapWith<CreateCashbackCommand>
{
    public decimal CashbackPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashbackVM, CreateCashbackCommand>();
    }
}
