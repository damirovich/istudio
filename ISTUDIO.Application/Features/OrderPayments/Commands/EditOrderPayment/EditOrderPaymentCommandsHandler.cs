namespace ISTUDIO.Application.Features.OrderPayments.Commands.EditOrderPayment;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditOrderPaymentCommandsHandler : IRequestHandler<EditOrderPaymentCommands, ResModel>
{
    private readonly IAppDbContext  _appDbContext;
    private readonly IMapper _mapper;

    public EditOrderPaymentCommandsHandler(IAppDbContext appDbContext, IMapper mapper)
        => (_appDbContext, _mapper) = (appDbContext, mapper);

    public async Task<ResModel> Handle(EditOrderPaymentCommands command, CancellationToken cancellationToken)
    {
        try
        {
            var orderPay = _mapper.Map<OrderPaymentEntity>(command);

            _appDbContext.OrderPayments.Update(orderPay);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch(Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
