namespace ISTUDIO.Application.Features.CashUsers.Commands.DeleteCashUsers;

using ResModel = Result;
public class DeleteCashUserCommand : IRequest<ResModel>
{
    public int CashbackId { get; set; }

    public class Handler : IRequestHandler<DeleteCashUserCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(DeleteCashUserCommand command, CancellationToken cancellationToken)
        {
            try 
            {
                var existingCashUser = await _appDbContext.UserCashbacks
                   .FirstOrDefaultAsync(m => m.Id == command.CashbackId);

                if (existingCashUser == null)
                    return ResModel.Failure(new[] { "Данные не найдены" });

                _appDbContext.UserCashbacks.Remove(existingCashUser);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }
}
