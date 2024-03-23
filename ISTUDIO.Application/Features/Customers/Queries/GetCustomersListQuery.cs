using AutoMapper.QueryableExtensions;
using ISTUDIO.Application.Features.Customers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.Customers.Queries;
using ResModel = PaginatedList<CustomerResponseDTO>;
public class GetCustomersListQuery : IRequest<ResModel>
{
    [Required]
    public PaginatedParameters Parameters { get; set; }

    public class Handler : IRequestHandler<GetCustomersListQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetCustomersListQuery query, CancellationToken cancellationToken)
        {
            var cusomter = _appDbContext.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ProjectTo<CustomerResponseDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.Parameters.PageNumber, query.Parameters.PageSize);

            return await cusomter;
        }
    }
}
