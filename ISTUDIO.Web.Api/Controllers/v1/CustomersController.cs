using ISTUDIO.Application.Features.CustomerImages.Commands;
using ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;
using ISTUDIO.Application.Features.Customers.Commands.DeleteCustomers;
using ISTUDIO.Application.Features.Customers.Commands.EditCustomers;
using ISTUDIO.Application.Features.Customers.Queries;
using ISTUDIO.Contracts.Features.Customers;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class CustomersController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IFileStoreService _fileStoreService;
    public CustomersController(IMapper mapper, IFileStoreService fileStoreService)
    {
        _mapper = mapper;
        _fileStoreService = fileStoreService;
    }

    /// <summary>
    /// Идентификация клиента загружается фотографии клиента 
    /// Фото паспорта с передней стороны
    /// Фото паспорта обратной стороны
    /// Фото селфи клиента с паспортом
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> IdentifyCustomers([FromForm] string userId, List<IFormFile> photoCustomers)
    {
        try
        {
            var customerImages = new List<CustomerImagesDTO>();

            foreach (var photo in photoCustomers)
            {
                if (photo != null)
                {
                    var photoUrl = await _fileStoreService.SaveImage(photo);

                    // Другие свойства из объекта IFormFile
                    var typeImg = photo.ContentType; // Пример типа изображения
                    var name = photo.FileName; // Пример имени файла

                    customerImages.Add(new CustomerImagesDTO
                    {
                        Url = photoUrl,
                        TypeImg = typeImg,
                        Name = name, 
                        UserId = userId
                    });
                }
            }

            var result = await Mediator.Send(new CreateCustomerImgCommand { CustomerImages = customerImages });
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
    /// Метод для проверки клиент прошел ли идентификацию 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCustomerIdentification([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCustomersIdentificationQuery
            {
                UserId = userId
            }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(StatusCodes.Status404NotFound, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Получение списка Клиентов
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCustomers([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCustomersListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Добавление данных клиента
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCustomers([FromForm] CreateCustomersVM customers, List<IFormFile> photoCustomers)
    {
        try
        {
            var customerImages = new List<CustomerImagesDTO>();

            foreach (var photo in photoCustomers)
            {
                if (photo != null)
                {
                    var photoUrl = await _fileStoreService.SaveImage(photo);

                    // Другие свойства из объекта IFormFile
                    var typeImg = photo.ContentType; // Пример типа изображения
                    var name = photo.FileName; // Пример имени файла

                    customerImages.Add(new CustomerImagesDTO
                    {
                        Url = photoUrl,
                        TypeImg = typeImg,
                        Name = name
                    });
                }
            }

            var command = _mapper.Map<CreateCustomersCommand>(customers);
            command.CustomerImages = customerImages;

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
    /// Изменение данных клиента
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditCustomers([FromForm] EditCustomersVM customers, List<IFormFile> photoCustomers)
    {
        try
        {
            var customerImages = new List<CustomerImagesDTO>();

            foreach (var photo in photoCustomers)
            {
                if (photo != null)
                {
                    var photoUrl = await _fileStoreService.SaveImage(photo);

                    // Другие свойства из объекта IFormFile
                    var typeImg = photo.ContentType; // Пример типа изображения
                    var name = photo.FileName; // Пример имени файла

                    customerImages.Add(new CustomerImagesDTO
                    {
                        Url = photoUrl,
                        TypeImg = typeImg,
                        Name = name
                    });
                }
            }

            var command = _mapper.Map<EditCustomersCommand>(customers);
            command.CustomerImages = customerImages;

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
    /// Удаление данных клиента
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteCustomers([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCustomersCommand { CustomerId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
