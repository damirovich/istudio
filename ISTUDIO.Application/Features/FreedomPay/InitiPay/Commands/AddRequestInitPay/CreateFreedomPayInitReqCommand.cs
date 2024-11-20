
namespace ISTUDIO.Application.Features.FreedomPay.InitiPay.Commands.AddRequestInitPay;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateFreedomPayInitReqCommand : IRequest<ResModel>
{
    public string JsonData { get; set; }

    public class Handler : IRequestHandler<CreateFreedomPayInitReqCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(CreateFreedomPayInitReqCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var FreedomPayInitReq = new FreedomPayInitReqEntity()
                {
                    JsonData = command.JsonData
                };

                _appDbContext.FreedomPayInitReq.Add(FreedomPayInitReq);

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
