namespace ISTUDIO.Application.Features.Cashbacks.Commands.EditCashbacks;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditCashbackCommandHandler : IRequestHandler<EditCashbackCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public EditCashbackCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(EditCashbackCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cashback = _mapper.Map<CashbackEntity>(command);

            _appDbContext.Cashbacks.Update(cashback);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }

}
