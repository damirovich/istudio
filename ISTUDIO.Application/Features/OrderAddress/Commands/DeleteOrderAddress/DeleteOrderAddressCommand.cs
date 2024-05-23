
namespace ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;

using ResModel = Result;
public class DeleteOrderAddressCommand : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteOrderAddressCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(DeleteOrderAddressCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var orderAddress = await _appDbContext.OrderAddresses.FindAsync(command.Id);

                if (orderAddress == null)
                {
                    return ResModel.Failure(new[] { "Order address not found." });
                }

                _appDbContext.OrderAddresses.Remove(orderAddress);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
