namespace ISTUDIO.Application.Features.Magazines.Commands.DeleteMagazines;

using ResModel = Result;
public class DeleteMagazinesCommand : IRequest<ResModel>
{
    public int MagazineId { get; set; }

    public class Handler : IRequestHandler<DeleteMagazinesCommand, ResModel>
    {
        private readonly IAppDbContext _appDbContext;

        public Handler(IAppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<ResModel> Handle(DeleteMagazinesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingMagazines = await _appDbContext.Magazines
                    .FirstOrDefaultAsync(m => m.Id == command.MagazineId);

                if (existingMagazines == null)
                    return ResModel.Failure(new[] { "Магазин не найден" });

                _appDbContext.Magazines.Remove(existingMagazines);

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
