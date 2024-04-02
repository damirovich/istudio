
using AutoMapper;
using ISTUDIO.Application.Common.Models;
using ISTUDIO.Application.Features.FavoriteProducts.Commands;
using ISTUDIO.Application.Features.FavoriteProducts.Queries;
using ISTUDIO.Contracts.Features.FavoriteProducts;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

public class FavoriteProductsController : BaseController
{

    private readonly IMapper _mapper;

    public FavoriteProductsController(IMapper mapper) =>
            _mapper = mapper;
    /// <summary>
    /// Получение список продуктов в избранном
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFavoriteProducts([FromQuery] string userId)
    {
        try
        {
           var result = await Mediator.Send(new GetFavoriteProductsByUserIdQuery
            {
               UserId = userId
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Добавление продуктов в избранные пользователя
    /// </summary>
    /// <param name="favoriteProducts"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddFavoriteProducts([FromBody] CreateFavoriteProductsVM favoriteProducts)
    {
        try
        {
            var command = _mapper.Map<CreateFavoriteProductsCommand>(favoriteProducts);

            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return StatusCode(StatusCodes.Status503ServiceUnavailable,  result.Errors);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Удаление продуктов в избранном
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteFavoriteProducts([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteFavoriteProductsCommand { Id = Id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
