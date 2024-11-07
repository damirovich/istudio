using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayResponse;

public class CreateFreedomPayResultResponseCommand : IRequest<Result>, IMapWith<FreedomPayResultResponseEntity>
{
    public string Status { get; set; }
    public string Description { get; set; }
    public string Salt { get; set; }
    public string Sig { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFreedomPayResultResponseCommand, FreedomPayResultResponseEntity>();
    }
}
