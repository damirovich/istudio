namespace ISTUDIO.Application.Features.CashbackProduct.Commands;

using ResModel = Result;
public class DeleteCashbackProductCommand : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteCashbackProductCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler (IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<ResModel> Handle (DeleteCashbackProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingDelCashbackProduct = await _appDbContext.ProductCashbacks
                  .FirstOrDefaultAsync(c => c.Id == command.Id);

                if (existingDelCashbackProduct == null)
                    return ResModel.Failure(new[] { "ProductCashbacks не найден" });

                _appDbContext.ProductCashbacks.Remove(existingDelCashbackProduct);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex) 
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }

    public class DeleteCashbackProductCommandValidtor : AbstractValidator<DeleteCashbackProductCommand>
    {
        public DeleteCashbackProductCommandValidtor()
        {
            RuleFor(v => v.Id)
               .GreaterThan(0)
               .WithMessage("Id должен быть больше 0.");
        }
    }
}