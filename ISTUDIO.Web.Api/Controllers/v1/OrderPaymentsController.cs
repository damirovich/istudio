using ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.DeleteOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;
using ISTUDIO.Application.Features.OrderPayments.Queries;
using ISTUDIO.Contracts.Features.OrderPayments;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class OrderPaymentsController : BaseController
{
    private readonly ILogger<OrderPaymentsController> _logger;
    private readonly IMapper _mapper;

    public OrderPaymentsController(ILogger<OrderPaymentsController> logger, IMapper mapper)
        => (_logger, _mapper) = (logger, mapper);

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderPaymentList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderPaymentsQuery
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetOrderPaymentById([FromQuery] int id)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetOrderPaymentsByIdQuery
            {
                OrderPayId = id
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateOrderPayment([FromBody] CreateOrderPaymentVM orderPayment)
    {
        try
        {
            var command = _mapper.Map<CreateOrderPaymentCommands>(orderPayment);
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditOrderPayment([FromBody] EditOrderPaymentVM orderPayment)
    {
        try
        {
            var command = _mapper.Map<EditOrderPaymentCommands>(orderPayment);
            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteOrderPayment([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteOrderPaymentCommands { Id = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
