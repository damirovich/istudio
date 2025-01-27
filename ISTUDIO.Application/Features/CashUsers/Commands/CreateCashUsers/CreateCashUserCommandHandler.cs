namespace ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCashUserCommandHandler : IRequestHandler<CreateCashUserCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateCashUserCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle(CreateCashUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Маппинг команды на сущность
            var userCashback = _mapper.Map<UserCashbackEntity>(command);

            // Добавление сущности в контекст базы данных
            _appDbContext.UserCashbacks.Add(userCashback);

            // Сохранение изменений в базе данных
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
