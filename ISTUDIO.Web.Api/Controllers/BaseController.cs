

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Produces("application/json")]
[ApiController]

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
