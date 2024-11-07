
namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateFreedomPayResultRequestCommandHandler : IRequestHandler<CreateFreedomPayResultRequestCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateFreedomPayResultRequestCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateFreedomPayResultRequestCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<FreedomPayResultRequestEntity>(command);
            entity.CreatedDate = DateTime.Now;

            _appDbContext.FreedomPayResultRequests.Add(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}