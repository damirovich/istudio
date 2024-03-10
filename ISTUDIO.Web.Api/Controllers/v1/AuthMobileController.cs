using Asp.Versioning;
using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;
using ISTUDIO.Contracts.Features.UserManagement;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class AuthMobileController : BaseController
{
    [HttpGet]
    public async Task<ICsmActionResult> SendOTP(string phonesNumber)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new SendSmsCommand { PhonesNumber = phonesNumber }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
            
        }
    }
    [HttpPost]
    public async Task<ICsmActionResult> RegistrUser([FromBody] CreateUserMobleVM user)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new RegistrUsersMobileCommand 
            {
                PhoneNumber = user.PhoneNumber,
                OTPCode = user.CodeOTP,
                Roles = new List<string> { "MobileUser" }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
