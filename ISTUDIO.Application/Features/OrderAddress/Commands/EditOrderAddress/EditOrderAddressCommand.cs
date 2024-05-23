namespace ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;
using ResModel = Result;
public class EditOrderAddressCommand : IRequest<ResModel>
{
    public int Id { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public string? UserId { get; set; }
    public int? OrderId { get; set; }

    public class Handler : IRequestHandler<EditOrderAddressCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(EditOrderAddressCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var orderAddress = await _appDbContext.OrderAddresses.FindAsync(command.Id);

                if (orderAddress == null)
                {
                    return ResModel.Failure(new[] { "Order address not found." });
                }

                orderAddress.Region = command.Region;
                orderAddress.City = command.City;
                orderAddress.Address = command.Address;
                orderAddress.Comments = command.Comments;
                orderAddress.UserId = command.UserId;
                orderAddress.OrderId = command.OrderId;

                _appDbContext.OrderAddresses.Update(orderAddress);
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