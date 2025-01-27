namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.EditCashTrans;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditCashTransactionCommandHandler : IRequestHandler<EditCashTransactionCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public EditCashTransactionCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(EditCashTransactionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cashTran = _mapper.Map<CashbackTransactionEntity>(command);

            _appDbContext.CashbackTransactions.Update(cashTran);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch(Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
