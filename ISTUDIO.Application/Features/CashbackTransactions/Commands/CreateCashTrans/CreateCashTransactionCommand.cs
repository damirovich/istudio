using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;

public class CreateCashTransactionCommand : IRequest<Result>, IMapWith<CashbackTransactionEntity>
{
    public string UserId { get; set; }
    public int? OrderId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashTransactionCommand, CashbackTransactionEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // Установится автоматически
    }
}
