using ISTUDIO.Application.Features.Orders.Commands.CreateOrders;
using ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ISTUDIO.Application.Features.Orders.Queries;
using ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ISTUDIO.Contracts.Features.Orders;
using Microsoft.EntityFrameworkCore;


namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

public class OrdersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;
    private readonly IAppUserService _appUserService;
    private readonly ISmsNikitaService _smsNikitaService;
    public OrdersController(IMapper mapper, IAppDbContext appDbContext, IAppUserService appUserService, ISmsNikitaService smsNikitaService) =>
            (_mapper, _appDbContext, _appUserService, _smsNikitaService) = (mapper, appDbContext, appUserService, smsNikitaService);

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

            var productList = string.Join("\n", orders.ProductOrders.Select(p =>
                     $"Название: {p.Name}, Количество: {p.QuantyProductCart}, Модель : {p.Model} сом"));

            //var smsRequest = new SmsNikitaRequestModel
            //{
            //    Id = Guid.NewGuid().ToString("N"),
            //    Text = $"Новый заказ в marketkg\n" +
            //           $"Номер заказа {result.OrderId}\n" +
            //           $"Клиент {user.UserPhoneNumber} Товары: {productList}\n" +
            //           $"Общее количество продуктов заказа {orders.TotalQuantyProduct}\n" +
            //           $"Дата заказа {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}",
            //    Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
            //    Phones = orderNotification.Select(o => o.PhoneNumber).ToArray()
            //};

            await Mediator.Send(new CreateSmsNikitaReqCommand
            {
                PhonesNumber =  orderNotification.Select(o=>o.PhoneNumber).ToList() ,
                Message = $"Новый заказ в marketkg\n" +
                          $"Номер заказа {result.OrderId}\n" +
                          $"Клиент {user.UserPhoneNumber} Товары: {productList}\n" +
                          $"Общее количество продуктов заказа {orders.TotalQuantyProduct}\n" +
                          $"Дата заказа {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}",
            });
            //await _smsNikitaService.SendSms(smsRequest);

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

    /// <summary>
    /// Добавление фотографии квитанции к заказу
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReceiptPhoto([FromBody] AddReceiptPhotoOrderVM model)
    {
        try
        {
            var command = _mapper.Map<AddReceipPhotoOrdersCommand>(model);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
