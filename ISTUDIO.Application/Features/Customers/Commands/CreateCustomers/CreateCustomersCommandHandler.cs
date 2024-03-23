namespace ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
internal class CreateCustomersCommandHandler  : IRequestHandler<CreateCustomersCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateCustomersCommandHandler(IAppDbContext appDbContext, IMapper mapper) 
        => (_appDbContext, _mapper)= (appDbContext, mapper);

    public async Task<ResModel> Handle(CreateCustomersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Маппинг команды на сущность
            var customerEntity = _mapper.Map<CustomersEntity>(command);

            // Добавление сущности в контекст базы данных
            _appDbContext.Customers.Add(customerEntity);

            // Сохранение всех изменений в базе данных одновременно
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
