using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Magazines.DTOs;

namespace ISTUDIO.Application.Features.Magazines.Queries;

using ResModel = PaginatedList<MagazinesDTO>;
public class GetMagazineListQuery : IRequest<ResModel>
{    
    public PaginatedParameters Parameters { get; set; }
    public class Handler : IRequestHandler<GetMagazineListQuery, ResModel>
    {

        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public Handler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResModel> Handle(GetMagazineListQuery query, CancellationToken cancellationToken)
        {
            var magazine = await _appDbContext.Magazines
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(m => m.IsActive == true)
                .ProjectTo<MagazinesDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return magazine;
        }
    }
}
