using ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Application.Features.SmsNikita.DTOs;
using ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;
using ISTUDIO.Contracts.Features.UserManagement;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class AuthController : BaseController
{
    /// <summary>
    /// Отправка ОТП кода в номер телефона
    /// </summary>
    /// <param name="phonesNumber"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SendOTP([FromForm] string phonesNumber)
    {
        try
        {
            //Проверка для AppStore 
            if (phonesNumber == "996700123456")
                return Ok(new SmsSendResponseDTO() { OTP = 123456, MessageStatus = "Сообщения успешно приняты к отправке" });
            //Отправка смс внутри MarketKG
            //  var result = await Mediator.Send(new SendSmsCommand { PhonesNumber = phonesNumber });
            //Добавление записи в таблицу SmsNikitaRequests с статусом StatusSendSMS = 0
            var otp = new Random().Next(100000, 1000000);

            var result = await Mediator.Send(new CreateSmsNikitaReqCommand 
            { 
                PhonesNumber = new List<string> { phonesNumber },
                Message = $"Код авторизации {otp}",
            });
            if (result.Succeeded)
            {
                return Ok(new SmsSendResponseDTO() { OTP = otp, MessageStatus = "Сообщения успешно приняты к отправке" });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

        }
    }
    [HttpPost]
    public async Task<IActionResult> RegistrUser([FromBody] CreateUserMobleVM user)
    {
        try
        {
            return Ok(await Mediator.Send(new RegistrUsersMobileCommand
            {
                PhoneNumber = user.PhoneNumber,
                OTPCode = user.CodeOTP,
                HasAgreedToPrivacyPolicy = user.HasAgreedToPrivacyPolicy,
                ConsentToTheUserAgreement = user.ConsentToTheUserAgreement,
                Roles = new List<string> { "MobileUser" }
            }));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
