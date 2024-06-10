using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Domain.EntityModel;
using System.Threading;


using ResModel = CreateOrderResponseDTO;
public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;

    public CreateOrdersCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService)
    {
        _appDbContext = appDbContext;
        _fileStoreService = fileStoreService;
    }

    public async Task<ResModel> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            string photoFilePath = string.Empty;
            if (command.ReceiptPhoto != null && command.ReceiptPhoto.Length > 0)
            {
                photoFilePath = await _fileStoreService.SaveImage(command.ReceiptPhoto);
            }
            var orderEntity = new OrderEntity
            {

                Status = "OrderProcessing",
                ShippingAddress = $"{command.OrderAddress.Region} {command.OrderAddress.City} {command.OrderAddress.Address}",
                TotalPrice = command.TotalAmount,
                TotalQuantyProduct = command.TotalQuantyProduct,
                PaymentMethod = command.PaymentMethod,
                UserId = command.UserId,
                ReceiptPhoto = photoFilePath
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
                    throw new BadRequestException("One or more products not found." );
                }
            }
            await _appDbContext.OrderAddresses.AddAsync(orderAddress, cancellationToken);
            await _appDbContext.Orders.AddAsync(orderEntity, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new ResModel { OrderId = orderEntity.Id };
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Error add Orders or OrderDetails {ex.Message}");
        }
    }
}
