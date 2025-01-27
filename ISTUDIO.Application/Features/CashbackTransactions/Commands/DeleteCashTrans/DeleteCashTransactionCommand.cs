namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.DeleteCashTrans;
using ResModel = Result;
public class DeleteCashTransactionCommand : IRequest<ResModel>
{
    public int CashTranId { get; set; }

    public class Handler : IRequestHandler<DeleteCashTransactionCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(DeleteCashTransactionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingCashTran = await _appDbContext.CashbackTransactions
                  .FirstOrDefaultAsync(m => m.Id == command.CashTranId);

                if (existingCashTran == null)
                    return ResModel.Failure(new[] { "Данные не найдены" });

                _appDbContext.CashbackTransactions.Remove(existingCashTran);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch(Exception ex) 
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }
}
