namespace ISTUDIO.Application.Features.Products.Queries;

using ResModel = List<DTOs.ProductImagesDTO>;
public class GetProductPhotosByIdQuery : IRequest<ResModel>
{
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<GetProductPhotosByIdQuery, ResModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext appDbContext, IMapper mapper)
            => (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<ResModel> Handle(GetProductPhotosByIdQuery query, CancellationToken cancellationToken)
        {
            var productImages = await _appDbContext.ProductImages.Where(p=>p.ProductId == query.ProductId).ToListAsync();

            if (productImages == null)
            {
                throw new NotFoundException("Продукт не найден");
            }

            var photosDto = _mapper.Map<ResModel>(productImages);

            return photosDto;
        }
    }
}
