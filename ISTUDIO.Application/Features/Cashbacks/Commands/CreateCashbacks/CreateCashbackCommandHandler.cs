
namespace ISTUDIO.Application.Features.Cashbacks.Commands.CreateCashbacks;

using AutoMapper;
using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCashbackCommandHandler : IRequestHandler<CreateCashbackCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateCashbackCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateCashbackCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Маппинг команды на сущность
            var cashback = _mapper.Map<CashbackEntity>(command);

            // Добавляем транзакцию в контекст базы данных
            _appDbContext.Cashbacks.Add(cashback);

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
