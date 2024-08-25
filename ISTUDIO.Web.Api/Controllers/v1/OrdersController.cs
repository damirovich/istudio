using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Commands.DeleteOrders;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;
using ISTUDIO.Application.Features.Orders.DTOs;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Contracts.Features.Orders;
using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Globalization;

namespace ISTUDIO.Web.Api.Controllers.v1;

public class OrdersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    public OrdersController(IMapper mapper, UserManager<AppUser> userManager) =>
            (_mapper, _userManager) = (mapper, userManager);


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

                var filteredItems = orders
                 .Where(o => o.Id.ToString().Contains(searchTerm) ||
                             (isDateSearch && o.CreateDate.Date == searchDate.Date) || // Поиск по точной дате
                             (internalStatus != null && o.Status == internalStatus) || // Поиск по статусу
                             o.TotalAmount.ToString().Contains(searchTerm) ||
                             (!string.IsNullOrEmpty(o.UserPhoneNumber) && o.UserPhoneNumber.Contains(searchTerm))
                            ).ToList();

                // Создание нового PaginatedList с отфильтрованными элементами
               var paginatedList = new PaginatedList<OrderResponseDTO>(
                    filteredItems,
                    filteredItems.Count,
                    page.PageNumber,
                    page.PageSize
                );

                return new CsmActionResult(paginatedList);
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
                return new CsmActionResult(result);

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
