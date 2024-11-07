using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;
using ResModel = Result;
public class CreateFreedomPayInitRequestCommandHandler : IRequestHandler<CreateFreedomPayInitRequestCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateFreedomPayInitRequestCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateFreedomPayInitRequestCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<FreedomPayInitRequestEntity>(command);
            entity.CreatedDate = DateTime.UtcNow;

            _appDbContext.FreedomPayInitRequests.Add(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}

