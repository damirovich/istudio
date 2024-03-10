using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Common.Interfaces;

public interface ISmsNikitaService
{
    Task<SmsNikitaResponseModel> SendSms(SmsNikitaRequestModel model);
}
