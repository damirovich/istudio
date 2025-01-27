namespace ISTUDIO.Application.Features.Cashbacks.Commands.DeleteCashbacks;
using ResModel = Result;
public class DeleteCashbackCommand : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteCashbackCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        public Handler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<ResModel> Handle (DeleteCashbackCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingCashback= await _appDbContext.Cashbacks
                 .FirstOrDefaultAsync(m => m.Id == command.Id);

                if (existingCashback == null)
                    return ResModel.Failure(new[] { "Данные не найдены" });

                _appDbContext.Cashbacks.Remove(existingCashback);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.Message });
            }
        }
    }

    public class DeleteCashbackCommandValidator : AbstractValidator<DeleteCashbackCommand>
    {
        public DeleteCashbackCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("CashbackId не должен быть пустым.")
             .GreaterThan(0).WithMessage("CashbackId должен быть положительным числом.");
        }
    }
}
