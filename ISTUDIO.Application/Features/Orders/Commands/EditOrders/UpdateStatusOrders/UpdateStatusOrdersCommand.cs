
namespace ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class UpdateStatusOrdersCommand : IRequest<ResModel>
{
    public int OrderId { get; set; }
    /// <summary>
    /// "OrderProcessing" Новый
    /// "OrderPaid" Оплачен
    /// "OrderShipped" Отправлено
    /// "OrderDelivered" Доставлено
    /// "OrderCompleted" Завершен
    /// "OrderCanceled" Возврат
    /// "OrderReturned" Отменен
    /// "OrderRejected" Отклонено
    /// </summary>
    public string OrderStatus { get; set; }   

    public class Handler : IRequestHandler<UpdateStatusOrdersCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(UpdateStatusOrdersCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingOrder = await _appDbContext.Orders
                    .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                    .Include(o => o.StatusHistories) // Включение истории статусов
                    .FirstOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);

                if (existingOrder == null)
                    return ResModel.Failure(new[] { "Order не найдена" });

                // Проверка: был ли такой статус уже присвоен в истории
                if (existingOrder.StatusHistories.Any(sh => sh.Status == command.OrderStatus))
                {
                    return ResModel.Failure(new[] { $"Статус '{command.OrderStatus}' уже был присвоен заказу ранее." });
                }
                // Сохраните предыдущий статус в истории

                //Предыдущая версия 
                //var statusHistory = new OrderStatusHistoryEntity
                //{
                //    OrderId = existingOrder.Id,
                //    Status = existingOrder.Status,
                //    ChangeDate = DateTime.Now
                //};

                var statusHistory = new OrderStatusHistoryEntity
                {
                    OrderId = existingOrder.Id,
                    Status = "",
                    ChangeDate = DateTime.Now
                };

                existingOrder.StatusHistories.Add(statusHistory);

                // Проверка и обновление количества товара при изменении статуса на "OrderShipped"
                if (command.OrderStatus == "OrderShipped")
                {
                    foreach (var detail in existingOrder.Details)
                    {
                        var product = detail.Product;
                        if (product.QuantityInStock < detail.Quantity)
                        {
                            return ResModel.Failure(new[] { $"Недостаточное количество продукта: {product.Name} {product.Model}" });
                        }

                        product.QuantityInStock -= detail.Quantity;
                        _appDbContext.Products.Update(product);
                    }
                }
                else if(command.OrderStatus == "OrderCanceled" || command.OrderStatus== "OrderReturned")
                {
                    foreach (var detail in existingOrder.Details)
                    {
                        var product = detail.Product;
                        
                        product.QuantityInStock += detail.Quantity;
                        _appDbContext.Products.Update(product);
                    }
                }

                // Обновление статуса заказа
                //Предыдущая версия 
                // existingOrder.Status = command.OrderStatus;

                existingOrder.Status = new OrderStatusEntity();
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Error update status: {ex.Message}" });
            }
        }
    }
}
