using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.BakaiPay.CheckStatus;
using ResModel = Result;
public class CreateCheckStatusBakaiPayReqCommand : IRequest<ResModel>
{
    public int CreateTranId { get; set; }
    public class Handler : IRequestHandler<CreateCheckStatusBakaiPayReqCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(CreateCheckStatusBakaiPayReqCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var checkStatusEntity = new BakaiCheckStatusResEntity
                {
                    CreateTranId = command.CreateTranId,
                    Status = "Pending"
                };

                await _appDbContext.BakaiCheckStatusResEntities.AddAsync(checkStatusEntity);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
