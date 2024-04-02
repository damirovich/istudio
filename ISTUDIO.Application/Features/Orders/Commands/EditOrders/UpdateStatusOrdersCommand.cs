
namespace ISTUDIO.Application.Features.Orders.Commands.EditOrders;

using ResModel = Result;
public class UpdateStatusOrdersCommand : IRequest<ResModel>
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; }

    public class Handler : IRequestHandler<UpdateStatusOrdersCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(UpdateStatusOrdersCommand command, CancellationToken cancellationToken)
        {
            try 
            {
                var existingOrder = await _appDbContext.Orders.FindAsync(command.OrderId);
                
                if (existingOrder == null)
                    return ResModel.Failure(new[] { "Orders не найдена" });

                existingOrder.Status = command.OrderStatus;


                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Error update status {ex.Message}" });
            }
        }
    }
}
