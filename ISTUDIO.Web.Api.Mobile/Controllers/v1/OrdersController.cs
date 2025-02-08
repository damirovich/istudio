using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ISTUDIO.Contracts.Features.Orders;
using ISTUDIO.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

public class OrdersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;
    private readonly IAppUserService _appUserService;
    private readonly ISmsNikitaService _smsNikitaService;
    public OrdersController(IMapper mapper, IAppDbContext appDbContext, IAppUserService appUserService,
                            ISmsNikitaService smsNikitaService) =>
            (_mapper, _appDbContext, _appUserService, _smsNikitaService)
            =
            (mapper, appDbContext, appUserService, smsNikitaService);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateOrders([FromBody] CreateOrdersVM orders)
    {
        try
        {
            var command = _mapper.Map<CreateOrdersCommand>(orders);

            var result = await Mediator.Send(command);

            var orderNotification = await _appDbContext.OrderNotificationRecipients.ToListAsync();

            var user = await _appUserService.GetUserDetailsByUserIdAsync(orders.UserId);

            // Получаем список ID товаров из заказа
            var productIds = orders.ProductOrders.Select(s => s.Id).ToList();

            // Загружаем товары из базы данных
            var products = await _appDbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            // Создаём список товаров с данными из Products
            var productList = string.Join("\n", orders.ProductOrders.Select(p =>
            {
                var product = products.FirstOrDefault(prod => prod.Id == p.Id);
                return product != null
                    ? $"Название: {product.Name}, Количество: {p.QuantyProductCart}, Модель: {product.Model}, Цена: {product.Price} сом"
                    : $"Неизвестный товар (ID: {p.Id}), Количество: {p.QuantyProductCart}";
            }));

            var phones = orderNotification?.Select(o => o.PhoneNumber).ToList() ?? new List<string>();

            var smsMessage = new CreateSmsNikitaReqCommand
            {
                PhonesNumber = phones,
                Message = $"Новый заказ в MarketKG\n" +
                          $"Номер заказа: {result.OrderId}\n" +
                          $"Клиент: {user?.UserPhoneNumber ?? "Не указан"}\n" +
                          $"Товары:\n{productList}\n" +
                          $"Общее количество продуктов: {orders.TotalQuantyProduct}\n" +
                          $"Дата заказа: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            };

            // Отправка SMS
            await Mediator.Send(smsMessage);

            var smsRequest = new SmsNikitaRequestModel
            {
                Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Phones = orderNotification.Select(o => o.PhoneNumber).ToArray()
            };
            await _smsNikitaService.SendSms(smsRequest);
            
            return Ok(result);
        }
        catch (BadRequestException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrders([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = userId
            });

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrdersById([FromQuery] int orderId)
    {
        try
        {
            var result = await Mediator.Send(new GetOrderByIdQuery
            {
                OrderId = orderId
            });

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
    public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
    {
        try
        {
            var result = await Mediator.Send(new GetOrderDetailsByIdQuery { OrderId = orderId });
            return Ok(result);

        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    ///// <summary>
    ///// Добавление фотографии квитанции к заказу
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> AddReceiptPhoto([FromBody] AddReceiptPhotoOrderVM model)
    //{
    //    try
    //    {
    //        var command = _mapper.Map<AddReceipPhotoOrdersCommand>(model);
    //        var result = await Mediator.Send(command);

    //        if (result.Succeeded)
    //        {
    //            return Ok(result);
    //        }

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
    //    }
    //}
}
