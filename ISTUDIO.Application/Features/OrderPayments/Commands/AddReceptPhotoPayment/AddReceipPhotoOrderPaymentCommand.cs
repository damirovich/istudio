namespace ISTUDIO.Application.Features.OrderPayments.Commands.AddReceptPhotoPayment;

using ISTUDIO.Application.Common.Interfaces;

using ResModel = Result;
public class AddReceipPhotoOrderPaymentCommand : IRequest<ResModel>
{
    public int OrdersId { get; set; }
    public byte[] ReceiptPhoto { get; set; }

    public class Handler : IRequestHandler<AddReceipPhotoOrderPaymentCommand, ResModel>
    {
        private readonly IFileStoreService _fileStoreService;
        private readonly IAppDbContext _appDbContext;

        public Handler(IFileStoreService fileStoreService, IAppDbContext appDbContext)
        {
            _fileStoreService = fileStoreService;
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(AddReceipPhotoOrderPaymentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _appDbContext.Orders.FindAsync(command.OrdersId);
                if (order == null)
                {
                    return ResModel.Failure(new[] { "Order not found." });
                }
                var orderPay = await _appDbContext.OrderPayments.FirstOrDefaultAsync(s=>s.OrderId == command.OrdersId);

                if(orderPay == null)
                    return ResModel.Failure(new[] { "OrderPayments not found." });


                var photoFilePath = await _fileStoreService.SaveImage(command.ReceiptPhoto);
                orderPay.ReceiptPhoto = photoFilePath;
                // order.Status = "Order paid";

                _appDbContext.OrderPayments.Update(orderPay);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { $"Error adding receipt photo: {ex.Message}" });
            }
        }
    }
}
