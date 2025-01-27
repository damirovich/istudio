namespace ISTUDIO.Application.Features.CashbackProduct.Commands;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateCashbackProductCommand : IRequest<ResModel>
{
    public int ProductId { get; set; }
    public decimal MaxBonusPercent {  get; set; }

    public class Handler : IRequestHandler<CreateCashbackProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler (IAppDbContext appDbContext)
            =>_appDbContext = appDbContext;

        public async Task<ResModel> Handle(CreateCashbackProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Маппинг команды на сущность
                var cashbackProduct = new ProductCashbackEntity()
                {
                    ProductId = command.ProductId,
                    MaxBonusPercent = command.MaxBonusPercent
                };

                // Добавление сущности в контекст базы данных
                _appDbContext.ProductCashbacks.Add(cashbackProduct);

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

    public class CreateCashbackProductCommandValidator : AbstractValidator<CreateCashbackProductCommand>
    {
        public CreateCashbackProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId должен быть больше 0.");

            RuleFor(x => x.MaxBonusPercent)
                .InclusiveBetween(0, 100)
                .WithMessage("Максимальный процент бонуса должен быть в диапазоне от 0 до 100.");
        }
    }
}
