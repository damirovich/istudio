
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;

    public class CreateCashbackCommand : IRequest<Result>, IMapWith<CashbackEntity>
    {
        public decimal CashbackPercent { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCashbackCommand, CashbackEntity>();
        }

    }
