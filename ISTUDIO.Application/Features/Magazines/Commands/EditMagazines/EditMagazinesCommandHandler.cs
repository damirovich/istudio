namespace ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;

using ResModel = Result;
public class EditMagazinesCommandHandler : IRequestHandler<EditMagazinesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IFileStoreService _fileStoreService;

    public EditMagazinesCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileStoreService fileStoreService)
        => (_appDbContext, _mapper, _fileStoreService)
            = 
           (appDbContext, mapper, fileStoreService);

    public async Task<ResModel> Handle(EditMagazinesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            string photoFilePath = string.Empty;

            var editMaagaz = await _appDbContext.Magazines.FirstOrDefaultAsync(m => m.Id == command.MagazineId);

            if (editMaagaz == null)
                return ResModel.Failure(new[] { "Magazine не найден" });

            if (command.PhotoLogoURL != null && command.PhotoLogoURL.Length > 0)
            {
                photoFilePath = await _fileStoreService.SaveImage(command.PhotoLogoURL);
            }

            _mapper.Map(command, editMaagaz);

            editMaagaz.PhotoLogoURL = photoFilePath;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return ResModel.Success();

        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
