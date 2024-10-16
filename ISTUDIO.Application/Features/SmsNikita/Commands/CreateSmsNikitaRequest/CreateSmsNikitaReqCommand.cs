using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Application.Features.SmsNikita.DTOs;
using ISTUDIO.Domain.EntityModel;
using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Features.SmsNikita.Commands.CreateSmsNikitaRequest;
using ResModel = Result;
public class CreateSmsNikitaReqCommand : IRequest<ResModel>
{
    public List<string> PhonesNumber { get; set; }
    public string Message { get; set; }

    public class Handler : IRequestHandler<CreateSmsNikitaReqCommand, ResModel>
    {

        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) =>
            _appDbContext = appDbContext;
        public async Task<ResModel> Handle(CreateSmsNikitaReqCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var requestModel = new SmsNikitaRequestModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Text = command.Message,
                    Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Phones = command.PhonesNumber.ToArray()
                };

                var smsRequest = new SmsNikitaRequest
                {
                    Id = Guid.NewGuid(),
                    SenderCompany = "marketkg",
                    Time = DateTime.ParseExact(requestModel.Time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                    TextSms = requestModel.Text,
                    PhonesNumber = string.Join(",", requestModel.Phones),
                    Test = 0,
                    StatusSendSMS = false // Изменено на false, чтобы Worker Service мог обработать это сообщение
                };

                await _appDbContext.SmsNikitaRequests.AddAsync(smsRequest);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }
}
