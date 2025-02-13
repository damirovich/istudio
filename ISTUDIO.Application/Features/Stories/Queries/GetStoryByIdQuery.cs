using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Stories.DTOs;

namespace ISTUDIO.Application.Features.Stories.Queries;
using ResModel = StoriesResDTO;
public class GetStoryByIdQuery : IRequest<ResModel>
{
    public int StoryId { get; set; }

    public class Handler : IRequestHandler<GetStoryByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetStoryByIdQuery query, CancellationToken cancellationToken)
        {
            var story = await _appDbContext.Stories
                .AsNoTracking()
                .Where(s => s.Id == query.StoryId && s.IsActive)
                .ProjectTo<ResModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return story ?? throw new Exception("Сторис не найдена");
        }
    }
}
