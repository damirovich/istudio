using ISTUDIO.Application.Features.SmsNikita.DTOs;
using ISTUDIO.Domain.EntityModel;
using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Features.SmsNikita.Commands.BulkSms;

using ResModel = SmsSendResponseDTO;
public class SendBulkSmsCommand : IRequest<ResModel>
{
    public List<string> PhonesNumber { get; set; }
    public string Message { get; set; }

    public class Handler : IRequestHandler<SendBulkSmsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISmsNikitaService _smsNikitaService;

        public Handler(IAppDbContext appDbContext, ISmsNikitaService smsNikitaService)
        {
            _appDbContext = appDbContext;
            _smsNikitaService = smsNikitaService;
        }

        public async Task<ResModel> Handle(SendBulkSmsCommand command, CancellationToken cancellationToken)
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

                var result = await _smsNikitaService.SendSms(requestModel);

                var smsRequest = new SmsNikitaRequest
                {
                    SenderCompany = requestModel.Sender,
                    Time = DateTime.ParseExact(requestModel.Time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                    TextSms = requestModel.Text,
                    PhonesNumber = string.Join(",", requestModel.Phones) // Здесь изменено для хранения всех номеров
                };

                var smsStatus = _appDbContext.SmsNikitaStatuses.FirstOrDefault(x => x.Status == result.Status);

                if (smsStatus == null)
                {
                    throw new BadRequestException("Invalid SMS status returned from the SMS service.");
                }

                var smsResponse = new SmsNikitaResponse
                {
                    Phones =  result.Phones, // Сохраняем все номера
                    SmsCount = result.SmsCount,
                    Message = result.Message,
                    Request = smsRequest,
                    SmsStatus = smsStatus
                };

                await _appDbContext.SmsNikitaRequests.AddAsync(smsRequest, cancellationToken);
                await _appDbContext.SmsNikitaResponses.AddAsync(smsResponse, cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResModel { MessageStatus = smsStatus.Name };
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error! {ex.Message}");
            }
        }
    }
}
