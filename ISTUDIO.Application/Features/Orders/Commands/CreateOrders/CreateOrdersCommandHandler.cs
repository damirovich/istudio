using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Domain.EntityModel;
using System.Threading;


using ResModel = CreateOrderResponseDTO;
public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IFileStoreService _fileStoreService;
    private readonly ISmsNikitaService _smsNikitaService;
    public CreateOrdersCommandHandler(IAppDbContext appDbContext, IFileStoreService fileStoreService, ISmsNikitaService smsNikitaService)
    {
        _appDbContext = appDbContext;
        _fileStoreService = fileStoreService;
        _smsNikitaService = smsNikitaService;
    }

    public async Task<ResModel> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var item in command.ProductOrders)
            {
                var product = await _appDbContext.Products.FindAsync(item.Id);
                if (product != null)
                {
                    if (product.QuantityInStock < item.QuantyProductCart)
                    {
                        throw new BadRequestException($"{product.Name} {product.Model} в наличии не остался");
                    }
                }
                else
                {
                    throw new BadRequestException("Product not found.");
                }
            }

            

            string photoFilePath = string.Empty;
            if (command.ReceiptPhoto != null && command.ReceiptPhoto.Length > 0)
            {
                photoFilePath = await _fileStoreService.SaveImage(command.ReceiptPhoto);
            }
            //Предыдущая версия 
            //var orderEntity = new OrderEntity
            //{
            //    Status = "OrderProcessing",
            //    ShippingAddress = $"{command.OrderAddress.Region} {command.OrderAddress.City} {command.OrderAddress.Address}",
            //    TotalPrice = command.TotalAmount,
            //    TotalQuantyProduct = command.TotalQuantyProduct,
            //    PaymentMethod = command.PaymentMethod,
            //    UserId = command.UserId,
            //    ReceiptPhoto = photoFilePath,
            //    CreateDate = DateTime.Now
            //};

            var orderEntity = new OrderEntity
            {
               // Status = "OrderProcessing",
                ShippingAddress = $"{command.OrderAddress.Region} {command.OrderAddress.City} {command.OrderAddress.Address}",
                TotalPrice = command.TotalAmount,
                TotalQuantyProduct = command.TotalQuantyProduct,
               // PaymentMethod = command.PaymentMethod,
                UserId = command.UserId,
                ReceiptPhoto = photoFilePath,
                CreateDate = DateTime.Now
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
            var orderDetails = new List<OrderDetailEntity>();
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
                        Order = orderEntity,
                        MagazineId = (int)productEntity.MagazineId // Связь с магазином через продукт
                    };

                    orderEntity.Details.Add(orderDetail);
                    orderEntity.Products.Add(productEntity);

                    orderDetails.Add(orderDetail);
                    //await _appDbContext.OrderDetails.AddAsync(orderDetail, cancellationToken);
                }
                else
                {
                    throw new BadRequestException("One or more products not found.");
                }
            }
         
            await _appDbContext.OrderAddresses.AddAsync(orderAddress, cancellationToken);
            await _appDbContext.Orders.AddAsync(orderEntity, cancellationToken);
            await _appDbContext.OrderDetails.AddRangeAsync(orderDetails, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            // Добавляем запись в историю статусов

            //Предыдущая версия 
            //var statusHistory = new OrderStatusHistoryEntity
            //{
            //    OrderId = orderEntity.Id,
            //    Status = orderEntity.Status,
            //    ChangeDate = DateTime.UtcNow
            //};

            var statusHistory = new OrderStatusHistoryEntity
            {
                OrderId = orderEntity.Id,
                Status = "",
                ChangeDate = DateTime.UtcNow
            };

            await _appDbContext.OrderStatusHistories.AddAsync(statusHistory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new ResModel { OrderId = orderEntity.Id };
        }
        catch (BadRequestException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Error add Orders or OrderDetails: {ex.Message}");
        }


    }
}
