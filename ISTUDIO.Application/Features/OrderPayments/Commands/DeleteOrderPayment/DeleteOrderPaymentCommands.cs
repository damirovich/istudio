namespace ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;
using ResModel = Result;
public class DeleteOrderPaymentCommands : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteOrderPaymentCommands, ResModel>
    {
        private IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(DeleteOrderPaymentCommands commnad, CancellationToken cancellationToken)
        {
            try
            {
                var existingOrderPay = await _appDbContext.OrderPayments.FirstOrDefaultAsync(x => x.Id == commnad.Id);

                if (existingOrderPay == null) 
                {
                    return ResModel.Failure(new[] { "OrderPayments не найден" });
                }

                _appDbContext.OrderPayments.Remove(existingOrderPay);

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
