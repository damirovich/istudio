namespace ISTUDIO.Application.Features.Discounts.Commands.DeleteDiscounts;

using ISTUDIO.Application.Common.Interfaces;

using ResModel = Result;
public class DeleteDiscountsCommand : IRequest<ResModel>
{
    public int DiscountId { get; set; }

    public class Handler : IRequestHandler<DeleteDiscountsCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResModel> Handle(DeleteDiscountsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingDiscounts = await _appDbContext.Discounts
                      .FirstOrDefaultAsync(c => c.Id == command.DiscountId);

                if (existingDiscounts == null)
                    return ResModel.Failure(new[] { "Discounts не найден" });

                _appDbContext.Discounts.Remove(existingDiscounts);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
