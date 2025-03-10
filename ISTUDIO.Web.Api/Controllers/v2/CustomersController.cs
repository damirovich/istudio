using ISTUDIO.Application.Features.CustomerImages.Commands;
using ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;
using ISTUDIO.Application.Features.Customers.Commands.DeleteCustomers;
using ISTUDIO.Application.Features.Customers.Commands.EditCustomers;
using ISTUDIO.Application.Features.Customers.Queries;
using ISTUDIO.Application.Helpers;
using ISTUDIO.Contracts.Features.Customers;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class CustomersController : BaseController2
{
    private readonly ILogger<CustomersController> _logger;
    private readonly IMapper _mapper;
    public readonly IFileStoreService _fileStoreService;

    public CustomersController(ILogger<CustomersController> logger, IMapper mapper, IFileStoreService fileStoreService) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
        _fileStoreService = fileStoreService;
    }

    /// <summary>
    /// Идентификация клиента: загрузка фотографий
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="photoCustomers">Список фотографий клиента</param>
    /// <returns>Результат операции</returns>
    [HttpPost("identify")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> IdentifyCustomers([FromForm] string userId, List<IFormFile> photoCustomers)
    {
        var customerImages = await ProcessCustomerImages(userId, photoCustomers);
        var result = await Mediator.Send(new CreateCustomerImgCommand
        {
            CustomerImages = customerImages
        });
        return new CsmActionResult(result);
    }

    /// <summary>
    /// Проверка идентификации клиента
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Статус идентификации</returns>
    [HttpGet("identification-status")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCustomerIdentification([FromQuery] string userId)
    {
        return await HandleQuery(new GetCustomersIdentificationQuery { UserId = userId });
    }

    /// <summary>
    /// Получение всех фотографий паспортов который отправили на идентификациию
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Статус идентификации</returns>
    [HttpGet("identification-status")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCustomerIdentListQuery([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetCustomerIdentListQuery 
        {
            Parameters = new PaginatedParameters 
            { 
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            } 
        });
    }


    /// <summary>
    /// Получение списка клиентов
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCustomers([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetCustomersListQuery 
        { 
            Parameters = new PaginatedParameters 
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize 
            } 
        });
    }

    /// <summary>
    /// Добавление нового клиента
    /// </summary>
    /// <param name="customers">Данные клиента</param>
    /// <param name="photoCustomers">Фотографии клиента</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCustomers([FromForm] CreateCustomersVM customers, List<IFormFile> photoCustomers)
    {
        var customerImages = await ProcessCustomerImages(null, photoCustomers);
        var command = _mapper.Map<CreateCustomersCommand>(customers);
        command.CustomerImages = customerImages;
        return await HandleCommand(command);
    }

    /// <summary>
    /// Изменение данных клиента
    /// </summary>
    /// <param name="customers">Данные клиента</param>
    /// <param name="photoCustomers">Фотографии клиента</param>
    /// <returns>Результат операции</returns>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditCustomers([FromForm] EditCustomersVM customers, List<IFormFile> photoCustomers)
    {
        var customerImages = await ProcessCustomerImages(null, photoCustomers);
        var command = _mapper.Map<EditCustomersCommand>(customers);
        command.CustomerImages = customerImages;
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="Id">Идентификатор клиента</param>
    /// <returns>Результат операции</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteCustomers([FromQuery] int Id)
    {
        return await HandleCommand(new DeleteCustomersCommand { CustomerId = Id });
    }

    private async Task<List<CustomerImagesDTO>> ProcessCustomerImages(string? userId, List<IFormFile> photoCustomers)
    {
        var customerImages = new List<CustomerImagesDTO>();
        foreach (var photo in photoCustomers)
        {
            if (photo != null)
            {
                var fileByte = await HelperServices.ConvertToByteArrayAsync(photo);
                var photoUrl = await _fileStoreService.SaveImage(fileByte);
                customerImages.Add(new CustomerImagesDTO
                {
                    Url = photoUrl,
                    TypeImg = photo.ContentType,
                    Name = photo.FileName,
                    UserId = userId
                });
            }
        }
        return customerImages;
    }
}

