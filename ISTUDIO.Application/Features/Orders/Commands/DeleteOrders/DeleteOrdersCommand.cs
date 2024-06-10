namespace ISTUDIO.Application.Features.Orders.Commands.DeleteOrders;

using ResModel = Result;
public class DeleteOrdersCommand : IRequest<ResModel>
{
    public int OrderId { get; set; }

    public class Handler : IRequestHandler<DeleteOrdersCommand, ResModel>
    { 
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(DeleteOrdersCommand command, CancellationToken cancellationToken)
        {
            try 
            {
                var existingOrderAddress = await _appDbContext.OrderAddresses.FirstOrDefaultAsync(x=>x.OrderId == command.OrderId);
                var existingOrder = await _appDbContext.Orders.FindAsync(command.OrderId);

                if (existingOrderAddress == null)
                    return ResModel.Failure(new[] { "OrderAddress не найдена" });

                if (existingOrder == null)
                    return ResModel.Failure(new[] { "Orders не найдена" });

                _appDbContext.OrderAddresses.Remove(existingOrderAddress);
                _appDbContext.Orders.Remove(existingOrder);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Error delete orders {ex.Message}" });
            }
        }
    }
}
