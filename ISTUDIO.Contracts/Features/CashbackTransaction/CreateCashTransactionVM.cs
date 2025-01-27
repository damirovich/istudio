
using ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;

namespace ISTUDIO.Contracts.Features.CashbackTransaction;

public class CreateCashTransactionVM : IMapWith<CreateCashTransactionCommand>
{
    public string UserId { get; set; }
    public int? OrderId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCashTransactionVM,  CreateCashTransactionCommand>();
    }
}
