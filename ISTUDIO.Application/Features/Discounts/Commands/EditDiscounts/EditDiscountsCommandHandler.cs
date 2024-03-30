namespace ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;
using ResModel = Result;
public class EditDiscountsCommandHandler : IRequestHandler<EditDiscountsCommand, ResModel> 
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public EditDiscountsCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<ResModel> Handle(EditDiscountsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingDiscounts = await _appDbContext.Discounts
                   .FirstOrDefaultAsync(c => c.Id == command.Id);

            if (existingDiscounts == null)
                return ResModel.Failure(new[] { "Discounts не найден" });

            _mapper.Map(command, existingDiscounts);

            _appDbContext.Discounts.Update(existingDiscounts);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
