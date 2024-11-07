using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddResponseInitPay;
using ResModel = Result;
public class CreateFreedomPayInitResponseCommandHandler : IRequestHandler<CreateFreedomPayInitResponseCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateFreedomPayInitResponseCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateFreedomPayInitResponseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<FreedomPayInitResEntity>(command);
            entity.CreatedDate = DateTime.UtcNow;

            _appDbContext.FreedomPayInitRespons.Add(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
