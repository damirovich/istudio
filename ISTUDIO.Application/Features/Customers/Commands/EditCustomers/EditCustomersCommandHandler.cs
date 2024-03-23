namespace ISTUDIO.Application.Features.Customers.Commands.EditCustomers;
using ResModel = Result;
internal class EditCustomersCommandHandler : IRequestHandler<EditCustomersCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public EditCustomersCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle (EditCustomersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingCustomers = await _appDbContext.Customers.Include(c => c.CustomerImages)
                      .FirstOrDefaultAsync(c => c.Id == command.Id);
            if (existingCustomers == null)
                return ResModel.Failure(new[] { "Customers не найдена" });

            _mapper.Map(command, existingCustomers);

            _appDbContext.Customers.Update(existingCustomers);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
