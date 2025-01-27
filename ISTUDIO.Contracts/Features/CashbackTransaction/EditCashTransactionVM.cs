using ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;

namespace ISTUDIO.Contracts.Features.CashbackTransaction;

public class EditCashTransactionVM : IMapWith<EditCashTransactionCommand>
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int? OrderId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashTransactionVM,  EditCashTransactionCommand>();
    }
}
