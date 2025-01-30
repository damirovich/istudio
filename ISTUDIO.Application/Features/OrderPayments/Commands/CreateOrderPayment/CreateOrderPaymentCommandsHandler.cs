namespace ISTUDIO.Application.Features.OrderPayments.Commands.CreateOrderPayment;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateOrderPaymentCommandsHandler : IRequestHandler<CreateOrderPaymentCommands, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateOrderPaymentCommandsHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateOrderPaymentCommands command, CancellationToken cancellationToken)
    {
        try
        { 
            var orderPayEntity = _mapper.Map<OrderPaymentEntity>(command);
            orderPayEntity.PaymentDate = DateTime.Now;

            _appDbContext.OrderPayments.Add(orderPayEntity);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch(Exception ex)
        {
            return ResModel.Failure(new[] {ex.Message});
        }
    }
}
