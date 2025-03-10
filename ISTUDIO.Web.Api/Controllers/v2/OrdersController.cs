using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Commands.DeleteOrders;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ISTUDIO.Contracts.Features.Orders;
using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]

public class OrdersController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppDbContext _appDbContext;

    public OrdersController(IMapper mapper, UserManager<AppUser> userManager, IAppDbContext appDbContext, ILogger<OrdersController> logger)
        : base(logger) =>
        (_mapper, _userManager, _appDbContext) = (mapper, userManager, appDbContext);

    /// <summary>
    /// Создание нового заказа
    /// </summary>
    /// <param name="orders">Данные заказа</param>
    /// <returns>Результат создания заказа</returns>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateOrdersVM orders)
        => await HandleCommand(_mapper.Map<CreateOrdersCommand>(orders));

    /// <summary>
    /// Получение заказов по UserId
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список заказов пользователя</returns>
    [HttpGet("user/{userId}")]
    public async Task<ICsmActionResult> GetByUserId([FromRoute] string userId)
        => await HandleQuery(new GetOrdersByUserIdQuery { UserId = userId });

    /// <summary>
    /// Получение списка всех заказов с пагинацией
    /// </summary>
    [HttpGet]
    public async Task<ICsmActionResult> GetList([FromQuery] PaginatedListVM page)
    {
        var orders = await Mediator.Send(new GetOrdersListQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });

        foreach (var item in orders.Items)
        {
            var user = await _userManager.FindByIdAsync(item.UserId);
            item.UserPhoneNumber = user?.PhoneNumber;
        }

        return new CsmActionResult(orders);
    }

    /// <summary>
    /// Получение детальной информации о заказе по Id
    /// </summary>
    [HttpGet("{orderId:int}/details")]
    public async Task<ICsmActionResult> GetDetails([FromRoute] int orderId)
        => await HandleQuery(new GetOrderDetailsByIdQuery { OrderId = orderId });

    /// <summary>
    /// Получение заказа по идентификатору
    /// </summary>
    [HttpGet("{orderId:int}")]
    public async Task<ICsmActionResult> GetById([FromRoute] int orderId)
        => await HandleQuery(new GetOrderByIdQuery { OrderId = orderId });

    /// <summary>
    /// Обновление статуса заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="orders">Данные для обновления статуса</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Статус успешно обновлён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    public async Task<ICsmActionResult> UpdateStatus([FromBody] UpdateStatusOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<UpdateStatusOrdersCommand>(orders);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return new CsmActionResult(result.Errors);

            var orderData = await _appDbContext.Orders
                .Include(d => d.Details).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(x => x.Id == orders.OrderId);

            if (orderData is null)
                return new CsmActionResult(new CsmReturnStatus(-1, "Заказ не найден"));

            var user = await _userManager.FindByIdAsync(orderData.UserId?.ToString());


            var newStatus = await Mediator.Send(new GetOrderStatusNameQuery() { InternalStatusCode = orderData.Status.NameEng });

            var smsStatuses = new[] { "OrderPaid", "OrderShipped", "OrderDelivered" };
           
            if (smsStatuses.Contains(orderData.Status.NameEng))
            {
                await Mediator.Send(new CreateSmsNikitaReqCommand
                {
                    PhonesNumber = new List<string> { user.PhoneNumber },
                    Message = $"Изменён статус вашего заказа №{orderData.Id}.\n" +
                              $"Статус заказа: {newStatus}\n" +
                              $"Общее количество товаров: {orderData.TotalQuantyProduct}"
                });
            }
            var orderNotification = await _appDbContext.OrderNotificationRecipients.ToListAsync();

            var productList = string.Join("\n", orderData.Details.Select(d =>
                $"Название: {d.Product.Name}, Количество: {d.Quantity}, Модель: {d.Product.Model}"));

            await Mediator.Send(new CreateSmsNikitaReqCommand
            {
                PhonesNumber = orderNotification.Select(o => o.PhoneNumber).ToList(),
                Message = $"Статус Заказа №{orderData.Id} был изменён.\n" +
                          $"Новый статус: {newStatus}\n" +
                          $"Клиент: {user?.PhoneNumber}\n" +
                          $"Товары:\n{productList}\n" +
                          $"Общее количество товаров: {orderData.TotalQuantyProduct}"
            });

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Удаление заказа
    /// </summary>
    [HttpDelete("{orderId:int}")]
    public async Task<ICsmActionResult> Delete([FromRoute] int orderId)
        => await HandleCommand(new DeleteOrdersCommand { OrderId = orderId });

    /// <summary>
    /// Добавление фотографии квитанции к заказу
    /// </summary>
    [HttpPost]
    public async Task<ICsmActionResult> AddReceiptPhoto([FromBody] AddReceiptPhotoOrderVM model)
    {
        var command = _mapper.Map<AddReceipPhotoOrdersCommand>(model);
        return await HandleCommand(command);
    }
}
