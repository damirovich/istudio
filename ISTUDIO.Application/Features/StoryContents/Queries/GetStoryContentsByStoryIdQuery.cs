using ISTUDIO.Application.Features.StoryContents.DTOs;

namespace ISTUDIO.Application.Features.StoryContents.Queries;

using AutoMapper.QueryableExtensions;
using ResModel = List<StoryContentResDTO>;
public class GetStoryContentsByStoryIdQuery : IRequest<ResModel>
{
    public int StoryId { get; set; }

    public class Handler : IRequestHandler<GetStoryContentsByStoryIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetStoryContentsByStoryIdQuery query, CancellationToken cancellationToken)
        {
            var contents = await _appDbContext.StoryContents
                .AsNoTracking()
                .Where(sc => sc.StoryId == query.StoryId)
                .OrderBy(sc => sc.Queue) // Сортировка по порядку
                .ProjectTo<StoryContentResDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return contents;
        }
    }
}