using ISTUDIO.Application.Features.Orders.DTOs;

namespace ISTUDIO.Application.Features.Orders.Commands.CreateOrders;

using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Domain.EntityModel;
using System.Threading;


using ResModel = CreateOrderResponseDTO;
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

            var status = await _appDbContext.OrderStatus.FirstOrDefaultAsync(x => x.Id == 1) ?? throw new NotFoundException("Статус заказа не найден");

            var orderEntity = new OrderEntity
            {

                Status = status,
                ShippingAddress = $"{command.OrderAddress.Region} {command.OrderAddress.City} {command.OrderAddress.Address}",
                TotalPrice = command.TotalAmount,
                TotalQuantyProduct = command.TotalQuantyProduct,
                UserId = command.UserId,
                CreateDate = DateTime.Now,
                OrderNumber = $"ORD-{DateTime.Now.Ticks}"
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
                        UnitPrice = productEntity.Price,
                        Order = orderEntity,
                        MagazineId = (int)productEntity.MagazineId // Связь с магазином через продукт
                    };

                    orderEntity.Details.Add(orderDetail);
                    orderEntity.Products.Add(productEntity);

                    orderDetails.Add(orderDetail);                    
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

            var statusHistory = new OrderStatusHistoryEntity
            {
                OrderId = orderEntity.Id,
                Status = status.NameEng,
                ChangeDate = DateTime.Now
            };

            await _appDbContext.OrderStatusHistories.AddAsync(statusHistory, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);


            var payMethods = await _appDbContext.PaymentMethods.AsNoTracking().ProjectTo<OrderPayMethodResDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return new ResModel {
                OrderId = orderEntity.Id,
                PaymentMethods = payMethods
            };
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
