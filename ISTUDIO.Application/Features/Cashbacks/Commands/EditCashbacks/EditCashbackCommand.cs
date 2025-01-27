using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;

public class EditCashbackCommand : IRequest<Result>, IMapWith<CashbackEntity>
{
    public int Id { get; set; }
    public decimal CashbackPercent { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public bool IsActive { get; set; } 

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashbackCommand, CashbackEntity>();
    }
}
