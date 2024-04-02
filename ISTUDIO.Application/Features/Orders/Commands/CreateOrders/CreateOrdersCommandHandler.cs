namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Domain.EntityModel;
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

    public async Task<ResModel> Handle(CreateOrdersCommand command, CancellationToken cancellation)
    {
        try
        {
            var orderEntity = new OrderEntity
            {

                Status = "New Order",
                ShippingAddress = command.ShippingAddress,
                TotalPrice = command.TotalAmount,
                TotalQuantyProduct = command.TotalQuantyProduct,
                UserId = command.UserId
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
                        UnitPrice = productDto.Price
                    };

                    _appDbContext.OrderDetails.Add(orderDetail);

                    //Добавление в OrderDetails продукт
                    orderEntity.Details.Add(orderDetail);

                    //Добавление в Order продукт
                    orderEntity.Products.Add(productEntity);
                }
                else
                {
                    // Если продукт не найден, вернуть ошибку
                    return ResModel.Failure(new[] { "One or more products not found." });
                }
            }
            _appDbContext.Orders.Add(orderEntity);

            await _appDbContext.SaveChangesAsync(cancellation);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { $"Error add Orders or OrderDetails {ex.Message}" });
        }
    }
}
