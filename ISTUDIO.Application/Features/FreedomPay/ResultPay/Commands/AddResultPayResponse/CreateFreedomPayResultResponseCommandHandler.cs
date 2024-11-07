using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayResponse;
using ResModel = Result;
public class CreateFreedomPayResultResponseCommandHandler : IRequestHandler<CreateFreedomPayResultResponseCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateFreedomPayResultResponseCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateFreedomPayResultResponseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<FreedomPayResultResponseEntity>(command);
            entity.CreatedDate = DateTime.Now;

            _appDbContext.FreedomPayResultResponses.Add(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}