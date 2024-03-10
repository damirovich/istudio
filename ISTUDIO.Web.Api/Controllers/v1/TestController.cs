using Asp.Versioning;
using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class TestController : BaseController
{
    [HttpGet("test")]
    public async Task<ICsmActionResult> TestWorking()
    {
        try
        {
            return new CsmActionResult(new CsmReturnStatus(0, "Этот щедерв искусства прекрасно работает"));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }

    }
}
