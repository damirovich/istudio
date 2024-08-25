namespace ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;

using ISTUDIO.Domain.EntityModel;
using ResModel = Result;
public class CreateMagazinesCommandHandler : IRequestHandler<CreateMagazinesCommand, ResModel>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IFileStoreService _fileStoreService;
    public CreateMagazinesCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileStoreService fileStoreService)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _fileStoreService = fileStoreService;
    }

    public async Task<ResModel> Handle(CreateMagazinesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            string photoFilePath = string.Empty;

            if (command.PhotoLogoURL != null && command.PhotoLogoURL.Length > 0)
            {
                photoFilePath = await _fileStoreService.SaveImage(command.PhotoLogoURL);
            }
            
            // Маппинг команды на сущность
            var magazine = _mapper.Map<MagazineEntity>(command);

            magazine.PhotoLogoURL = photoFilePath;

            // Добавление сущности в контекст базы данных
            _appDbContext.Magazines.Add(magazine);

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
