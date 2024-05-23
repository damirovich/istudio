namespace ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderAddress;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateOrderAddressCommand : IRequest<ResModel>
{
    public string Region { get; set; }
    public string City { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public string? UserId { get; set; }
    public int? OrderId { get; set; }

    public class Handler : IRequestHandler<CreateOrderAddressCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(CreateOrderAddressCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var orderAddress = new OrderAddressEntity
                {
                    Region = command.Region,
                    City = command.City,
                    Address = command.Address,
                    Comments = command.Comments,
                    UserId = command.UserId,
                    OrderId = command.OrderId
                };

                await _appDbContext.OrderAddresses.AddAsync(orderAddress);
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
