namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Domain.EntityModel;
using System.Threading;
using ResModel = Result;
public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateOrdersCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = new OrderEntity
            {

                Status = "New Order",
                ShippingAddress = $"{command.OrderAddress.Region} {command.OrderAddress.City} {command.OrderAddress.Address}",
                TotalPrice = command.TotalAmount,
                TotalQuantyProduct = command.TotalQuantyProduct,
                UserId = command.UserId
            };

            var orderAddress = new OrderAddressEntity
            {
                Region = command.OrderAddress.Region,
                City = command.OrderAddress.City,
                Address = command.OrderAddress.Address,
                Comments = command.OrderAddress.Comments,
                UserId = orderEntity.UserId,
                Orders = orderEntity
            };

            // Добавляем продукты к заказу
            foreach (var productDto in command.ProductOrders)
            {
                var productEntity = await _appDbContext.Products.FindAsync(productDto.Id);
                if (productEntity != null)
                {
                    var orderDetail = new OrderDetailEntity
                    {
                        Product = productEntity,
                        Quantity = productDto.QuantyProductCart,
                        UnitPrice = productDto.Price,
                        Order = orderEntity
                    };

                    orderEntity.Details.Add(orderDetail);
                    orderEntity.Products.Add(productEntity);

                    _appDbContext.OrderDetails.Add(orderDetail);
                }
                else
                {
                    return Result.Failure(new[] { "One or more products not found." });
                }
            }
            await _appDbContext.OrderAddresses.AddAsync(orderAddress, cancellationToken);
            await _appDbContext.Orders.AddAsync(orderEntity, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { $"Error add Orders or OrderDetails {ex.Message}" });
        }
    }
}
