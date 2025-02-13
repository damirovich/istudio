using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.StoryContents.DTOs;

namespace ISTUDIO.Application.Features.StoryContents.Queries;
using ResModel = StoryContentResDTO;
public class GetStoryContentByIdQuery : IRequest<ResModel>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetStoryContentByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetStoryContentByIdQuery query, CancellationToken cancellationToken)
        {
            var contents = await _appDbContext.StoryContents
              .AsNoTracking()
              .Where(sc => sc.Id == query.Id)
              .OrderBy(sc => sc.Queue) // Сортировка по порядку
              .ProjectTo<ResModel>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync(cancellationToken);

            return contents;
        }
    }
}
