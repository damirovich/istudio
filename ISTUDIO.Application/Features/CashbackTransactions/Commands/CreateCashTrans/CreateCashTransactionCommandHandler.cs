namespace ISTUDIO.Application.Features.CashbackTransactions.Commands.CreateCashTrans;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCashTransactionCommandHandler : IRequestHandler<CreateCashTransactionCommand, Result>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateCashTransactionCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle(CreateCashTransactionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Маппинг команды на сущность
            var transaction = _mapper.Map<CashbackTransactionEntity>(command);

            // Устанавливаем дату создания
            transaction.CreatedAt = DateTime.Now;

            // Добавляем транзакцию в контекст базы данных
            _appDbContext.CashbackTransactions.Add(transaction);

            // Сохраняем изменения в базе данных
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
