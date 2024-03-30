namespace ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateDiscountsCommandHandler : IRequestHandler<CreateDiscountsCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateDiscountsCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle(CreateDiscountsCommand command, CancellationToken cancellationToken)
    {
        try 
        {
            // Маппинг команды на сущность
            var discountEntity = _mapper.Map<DiscountEntity>(command);

            // Добавление сущности в контекст базы данных
            _appDbContext.Discounts.Add(discountEntity);

            // Сохранение всех изменений в базе данных одновременно
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.Message });
        }
    }
}
