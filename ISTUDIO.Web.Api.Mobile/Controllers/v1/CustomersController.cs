

using Asp.Versioning;
using AutoMapper;
using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Application.Features.CustomerImages.Commands;
using ISTUDIO.Application.Features.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

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
    public async Task<IActionResult> IdentifyCustomers([FromForm] string userId, List<IFormFile> photoCustomers)
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
                return Ok(result);

            return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
