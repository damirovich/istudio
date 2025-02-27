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
using System.Globalization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class OrdersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ISmsNikitaService _smsNikitaService;
    private readonly IAppDbContext _appDbContext;
    public OrdersController(IMapper mapper, UserManager<AppUser> userManager, ISmsNikitaService smsNikitaService, IAppDbContext appDbContext) =>
            (_mapper, _userManager, _smsNikitaService, _appDbContext) = (mapper, userManager, smsNikitaService, appDbContext);


    /// <summary>
    /// Добавление данных заказа
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrders([FromBody] CreateOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<CreateOrdersCommand>(orders);

            var result = await Mediator.Send(command);

            return new CsmActionResult(result);

        }
        catch (BadRequestException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Получение данных о заказе по UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrders([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = userId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Получение всех список заказов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrdersList([FromQuery] PaginatedListVM page)
    {
        try
        {
            var order = await Mediator.Send(new GetOrdersListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            });

            foreach (var item in order.Items)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                item.UserPhoneNumber = user?.PhoneNumber;
            }

            return new CsmActionResult(order);


        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Поиск в заказа
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetSearchOrders([FromQuery] PaginatedListVM page, string searchTerm)
    {
        try
        {
            var orders = await Mediator.Send(new GetSearchOrdersQuery
            {
                Parameters = new PaginationWithSearchParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize,
                    SearchTerm = searchTerm
                }
            });
            bool isDateSearch = false;
            DateTime searchDate;

            // Список поддерживаемых форматов дат
            string[] dateFormats = new[]
            {
                "dd-MM-yyyy",  "dd.MM.yyyy",    // День Месяц Год
                "yyyy-MM-dd",  "yyyy.MM.dd",    // Год Месяц День
                "MM-dd-yyyy",  "MM.dd.yyyy"     // Месяц День Год
            };
            // Массив статусов для проверки
            var statusMapping = new Dictionary<string, string>
            {
                { "Новый", "OrderProcessing" },     { "новый", "OrderProcessing" },
                { "Оплачен", "OrderPaid" },         { "оплачен", "OrderPaid" },
                { "Отправлено", "OrderShipped" },   { "отправлено", "OrderShipped" },
                { "Доставлено", "OrderDelivered" }, { "доставлено", "OrderDelivered" },
                { "Завершен", "OrderCompleted" },   { "завершен", "OrderCompleted" },
                { "Возврат", "OrderReturned" },     { "возврат", "OrderReturned" },
                { "Отменен", "OrderCanceled" },     { "отменен", "OrderCanceled" }
            };

            // Пытаемся распознать дату в любом из указанных форматов
            isDateSearch = DateTime.TryParseExact(searchTerm, dateFormats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out searchDate);

            // Получение номеров телефонов пользователей для каждого заказа
            foreach (var item in orders)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                item.UserPhoneNumber = user?.PhoneNumber;
            }

            // Фильтрация заказов по номеру телефона
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Проверка, является ли searchTerm пользовательским представлением статуса
                var internalStatus = statusMapping.ContainsKey(searchTerm) ? statusMapping[searchTerm] : null;

                //var filteredItems = orders
                // .Where(o => o.Id.ToString().Contains(searchTerm) ||
                //             (isDateSearch && o.CreateDate.Date == searchDate.Date) || // Поиск по точной дате
                //             (internalStatus != null && o.Status == internalStatus) || // Поиск по статусу
                //             o.TotalAmount.ToString().Contains(searchTerm) ||
                //             (!string.IsNullOrEmpty(o.UserPhoneNumber) && o.UserPhoneNumber.Contains(searchTerm))
                //            ).ToList();

                //// Создание нового PaginatedList с отфильтрованными элементами
                //var paginatedList = new PaginatedList<OrderResponseDTO>(
                //     filteredItems,
                //     filteredItems.Count,
                //     page.PageNumber,
                //     page.PageSize
                // );

                return new CsmActionResult();
            }
            return new CsmActionResult();
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение детальную информацию о заказе 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderDetails([FromQuery] int orderId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderDetailsByIdQuery
            {
                OrderId = orderId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Получение данные заказа по Id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> GetOrdersById([FromQuery] int orderId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderByIdQuery
            {
                OrderId = orderId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Обновление статуса заказа
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> UpdateStatusOrders([FromBody] UpdateStatusOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<UpdateStatusOrdersCommand>(orders);

            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                // Маппинг статусов
                var statusMapping = new Dictionary<string, string>
                    {
                        { "OrderProcessing", "Новый" },
                        { "OrderPaid", "Оплачен" },
                        { "OrderShipped", "Отправлено" },
                        { "OrderDelivered", "Доставлено" },
                        { "OrderCompleted", "Завершен" },
                        { "OrderReturned", "Возврат" },
                        { "OrderCanceled", "Отменен" }
                    };

                // Получаем список для уведомления
                var orderNotification = await _appDbContext.OrderNotificationRecipients.ToListAsync();

                // Загружаем информацию о заказе
                var orderData = await _appDbContext.Orders
                    .Include(d => d.Details)
                    .ThenInclude(d => d.Product)
                    .FirstOrDefaultAsync(x => x.Id == orders.OrderId);

                // Определяем новый статус на основании полученного в заказе

                //Предыдущая версия 
                //string newStatus;
                //if (!statusMapping.TryGetValue(orderData.Status, out newStatus))
                //{
                //    newStatus = "Unknown"; // Если статус не найден в маппинге
                //}
                string newStatus;
                if (!statusMapping.TryGetValue("", out newStatus))
                {
                    newStatus = "Unknown"; // Если статус не найден в маппинге
                }

                // Получаем данные о пользователе
                var user = await _userManager.FindByIdAsync(orderData.UserId?.ToString());

                // Формируем список товаров
                var productList = string.Join("\n", orderData.Details.Select(d =>
                         $"Название: {d.Product.Name}, Количество: {d.Quantity}, Модель: {d.Product.Model} сом"));

                //Отправляем клиенту смс о изменение статуса только при Оплачен, Отправлено, Доставлено
                var smsStatuses = new[] { "OrderPaid", "OrderShipped", "OrderDelivered" };
                if (smsStatuses.Contains(orders.OrderStatus))
                {
                    //Формируем сообщение для клиента
                    //var clientSmsRequest = new SmsNikitaRequestModel
                    //{
                    //    Id = Guid.NewGuid().ToString("N"),
                    //    Text = $"Изменен статус вашего заказа № {orderData.Id}.\n" +
                    //           $"Статус заказа: {newStatus}\n" +
                    //           $"Общее количество товаров в заказе: {orderData.TotalQuantyProduct}",
                    //    Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    //    Phones = [user.PhoneNumber]
                    //};

                    //Формируем сообщение для клиента
                    await Mediator.Send(new CreateSmsNikitaReqCommand
                    {
                        PhonesNumber = orderNotification.Select(o => o.PhoneNumber).ToList(),
                        Message = $"Изменен статус вашего заказа № {orderData.Id}.\n" +
                                  $"Статус заказа: {newStatus}\n" +
                                  $"Общее количество товаров в заказе: {orderData.TotalQuantyProduct}",
                    });

                    // Отправляем SMS
                   // await _smsNikitaService.SendSms(clientSmsRequest);

                }

                // Формируем сообщение для администраторов
                //var adminSmsRequest = new SmsNikitaRequestModel
                //{
                //    Id = Guid.NewGuid().ToString("N"),
                //    Text = $"Статус Заказа № {orderData.Id} был изменен.\n" +
                //           $"Новый статус: {newStatus}\n" +
                //           $"Клиент: {user.PhoneNumber}\n" +
                //           $"Товары:\n{productList}\n" +
                //           $"Общее количество товаров: {orderData.TotalQuantyProduct}",
                //    Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                //    Phones = orderNotification.Select(p => p.PhoneNumber).ToArray() // Отправляем администраторам
                //};

                // Формируем сообщение для администраторов
                await Mediator.Send(new CreateSmsNikitaReqCommand
                {
                    PhonesNumber = orderNotification.Select(o => o.PhoneNumber).ToList(),
                    Message = $"Статус Заказа № {orderData.Id} был изменен.\n" +
                              $"Новый статус: {newStatus}\n" +
                              $"Клиент: {user.PhoneNumber}\n" +
                              $"Товары:\n{productList}\n" +
                              $"Общее количество товаров: {orderData.TotalQuantyProduct}",
                });

                // Отправляем SMS
                //await _smsNikitaService.SendSms(adminSmsRequest);


                return new CsmActionResult(result);
            }

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Удаление данных заказа
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrdes([FromQuery] int orderId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteOrdersCommand { OrderId = orderId });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Добавление фотографии квитанции к заказу
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> AddReceiptPhoto([FromBody] AddReceiptPhotoOrderVM model)
    {
        try
        {
            var command = _mapper.Map<AddReceipPhotoOrdersCommand>(model);
            var result = await Mediator.Send(command);

            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
