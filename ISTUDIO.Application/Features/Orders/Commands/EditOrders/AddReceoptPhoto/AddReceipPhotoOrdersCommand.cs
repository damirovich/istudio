namespace ISTUDIO.Application.Features.Orders.Commands.EditOrders.AddReceoptPhoto;
using ResModel = Result;
public class AddReceipPhotoOrdersCommand  : IRequest<ResModel>
{
    public int OrdersId { get; set; }
    public byte[] ReceiptPhoto { get; set; }

    public class Handler : IRequestHandler<AddReceipPhotoOrdersCommand, ResModel>
    {
        private readonly IFileStoreService _fileStoreService;
        private readonly IAppDbContext _appDbContext;

        public Handler(IFileStoreService fileStoreService, IAppDbContext appDbContext)
        {
            _fileStoreService = fileStoreService;
            _appDbContext = appDbContext;
        }

        public async Task<ResModel> Handle(AddReceipPhotoOrdersCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _appDbContext.Orders.FindAsync(command.OrdersId);
                if (order == null)
                {
                    return ResModel.Failure(new[] { "Order not found." });
                }

                var photoFilePath = await _fileStoreService.SaveImage(command.ReceiptPhoto);
               // order.ReceiptPhoto = photoFilePath;
               // order.Status = "Order paid";

                _appDbContext.Orders.Update(order);
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
