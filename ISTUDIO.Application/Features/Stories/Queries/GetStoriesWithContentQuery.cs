using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Stories.DTOs;

namespace ISTUDIO.Application.Features.Stories.Queries;

using ResModel = List<StoriesWithContentResDTO>;

public class GetStoriesWithContentQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetStoriesWithContentQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetStoriesWithContentQuery query, CancellationToken cancellationToken)
        {
            var stories = await _appDbContext.Stories
                   .Include(d => d.StoryContents)
                   .AsNoTracking()
                   .Where(p => p.IsActive == true)
                   .OrderByDescending(c => c.Id)
                   .ProjectTo<StoriesWithContentResDTO>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            return stories;
        }
    }
}
