namespace ISTUDIO.Application.Features.Customers.Commands.DeleteCustomers;

using ISTUDIO.Application.Common.Interfaces;

using ResModel = Result;
public class DeleteCustomersCommand : IRequest<ResModel>
{
    public int CustomerId { get; set; }
    public class Handler : IRequestHandler<DeleteCustomersCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileStoreService _fileStoreService;
        public Handler(IAppDbContext appDbContext, IFileStoreService fileStoreService)
        {
            _appDbContext = appDbContext;
            _fileStoreService = fileStoreService;
        }

        public async Task<ResModel> Handle(DeleteCustomersCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingCustomers = await _appDbContext.Customers
                      .Include(c => c.CustomerImages)
                      .FirstOrDefaultAsync(c => c.Id == command.CustomerId);

                if (existingCustomers == null)
                    return ResModel.Failure(new[] { "Customer не найден" });

                _appDbContext.Customers.Remove(existingCustomers);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                if (existingCustomers.CustomerImages != null)
                {
                    foreach (var item in existingCustomers.CustomerImages)
                    {
                        _fileStoreService.DeleteImage(item.Url);
                    }
                }

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
