
namespace ISTUDIO.Application.Features.CashbackProduct.Commands;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class EditCashbackProductCommand : IRequest<ResModel>, IMapWith<ProductCashbackEntity>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal MaxBonusPercent { get; set; }

    public class Handler : IRequestHandler<EditCashbackProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(EditCashbackProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingProductCashback = await _appDbContext.ProductCashbacks
                 .FirstOrDefaultAsync(c => c.Id == command.Id);

                if (existingProductCashback == null)
                    return ResModel.Failure(new[] { "ProductCashbacks не найден" });

                // Обновляем свойства существующего объекта
                _mapper.Map(command, existingProductCashback);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch(Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }

        
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCashbackProductCommand, ProductCashbackEntity>();
    }

    public class EditCashbackProductCommandValidator : AbstractValidator<EditCashbackProductCommand>
    {
        public EditCashbackProductCommandValidator()
        {
            RuleFor(v=>v.Id)
                 .GreaterThan(0)
                 .WithMessage("ProductId должен быть больше 0.");

            RuleFor(x => x.ProductId)
               .GreaterThan(0)
               .WithMessage("ProductId должен быть больше 0.");

            RuleFor(x => x.MaxBonusPercent)
                .InclusiveBetween(0, 100)
                .WithMessage("Максимальный процент бонуса должен быть в диапазоне от 0 до 100.");
        }
    }
}
