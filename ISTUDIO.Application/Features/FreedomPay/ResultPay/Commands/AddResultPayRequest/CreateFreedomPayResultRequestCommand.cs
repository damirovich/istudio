using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FreedomPay.ResultPay.Commands.AddResultPayRequest;
using ResModel = Result;
public class CreateFreedomPayResultRequestCommand : IRequest<ResModel>
{
    public string JsonData { get; set; }

    public class Handler : IRequestHandler<CreateFreedomPayResultRequestCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(CreateFreedomPayResultRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                
                var FreedomPayResultRequest = new FreedomPayResultRequestEntity()
                {
                    JsonData = command.JsonData,
                    CreatedDate = DateTime.Now
                };
                _appDbContext.FreedomPayResultRequests.Add(FreedomPayResultRequest);
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
