
using ISTUDIO.Application.Features.SmsNikita.DTOs;
using ISTUDIO.Domain.Models;
using System.ComponentModel.DataAnnotations;
namespace ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;

using ResModel = SmsSendResponseDTO;
public class SendSmsCommand : IRequest<ResModel>
{
    [Required]
    public string PhonesNumber { get; set; }

    public class Handler : IRequestHandler<SendSmsCommand, ResModel>
    { 
        private readonly IAppDbContext _appDbContext;
        private readonly ISmsNikitaService _service;
        public Handler (ISmsNikitaService service, IAppDbContext appDbContext) => 
            (_service , _appDbContext) = (service, appDbContext);
        public async Task<ResModel> Handle(SendSmsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var otp = new Random().Next(100000, 1000000);
                var requestModel = new SmsNikitaRequestModel
                {
                    Login = "istudiokg",
                    Password = "7oPbRH5u",
                    Id = Guid.NewGuid().ToString("N"),
                    Sender = "SMSPRO.KG",
                    Text = $"Код авторизации {otp}",
                    Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Phones = command.PhonesNumber.Split(','),
                    Test = 1
                };

                //var result = await _service.SendSms(requestModel);

                //var smsRequest = new SmsNikitaRequest
                //{
                //    SenderCompany = requestModel.Sender,
                //    Time = DateTime.ParseExact(requestModel.Time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                //    TextSms = requestModel.Text,
                //    PhonesNumber = requestModel.Phones.FirstOrDefault(),
                //    Test = 0
                //};

                //var smsStatus = _appDbContext.SmsNikitaStatuses.FirstOrDefault(x => x.Status == result.Status);

                //var smsResponce = new SmsNikitaResponse
                //{
                //    Phones = result.Phones,
                //    SmsCount = result.SmsCount,
                //    Message = result.Message,
                //    Request = smsRequest,
                //    SmsStatus = smsStatus
                //};

                //await _appDbContext.SmsNikitaRequests.AddAsync(smsRequest);
                //await _appDbContext.SmsNikitaResponses.AddAsync(smsResponce);

                //await _appDbContext.SaveChangesAsync(cancellationToken);
                //if (result.Status == 0)
                //    return new ResModel { OTP = otp, MessageStatus = smsStatus.Name };

                //return new ResModel { MessageStatus = smsStatus.Name };
                return new ResModel { OTP = otp, MessageStatus = "Сообщения успешно приняты к отправке" };
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error! {ex.Message}");
            }
        }
    }
}
