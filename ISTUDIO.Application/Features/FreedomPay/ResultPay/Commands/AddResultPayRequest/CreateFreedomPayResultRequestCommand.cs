
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;

public class CreateFreedomPayResultRequestCommand : IRequest<Result>, IMapWith<FreedomPayResultRequestEntity>
{
    public int PgOrderId { get; set; }
    public int PgPaymentId { get; set; }
    public decimal PgAmount { get; set; }
    public string PgCurrency { get; set; }
    public decimal PgNetAmount { get; set; }
    public decimal PgPsAmount { get; set; }
    public decimal PgPsFullAmount { get; set; }
    public string PgPsCurrency { get; set; }
    public string PgDescription { get; set; }
    public int PgResult { get; set; }
    public DateTime PgPaymentDate { get; set; }
    public int PgCanReject { get; set; }
    public string PgUserPhone { get; set; }
    public short PgNeedPhoneNotification { get; set; }
    public string PgUserContactEmail { get; set; }
    public short PgNeedEmailNotification { get; set; }
    public int PgTestingMode { get; set; }
    public int PgCaptured { get; set; }
    public string PgReference { get; set; }
    public string PgCardPan { get; set; }
    public string PgAuthCode { get; set; }
    public string PgSalt { get; set; }
    public string PgSig { get; set; }
    public string PgPaymentMethod { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFreedomPayResultRequestCommand, FreedomPayResultRequestEntity>();
    }
}
