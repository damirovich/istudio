
namespace ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditCashUserCommandHandler : IRequestHandler<EditCashUserCommand, ResModel>  
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public EditCashUserCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(EditCashUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userCashback = _mapper.Map<UserCashbackEntity>(command);

            _appDbContext.UserCashbacks.Update(userCashback);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch(Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
