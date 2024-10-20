namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIUpdateProductActiveCommand : IRequest<ResponseAPI<ResModel>>
{
    public UpdateProductActiveVM ProductStatusUpdate { get; set; }

    public class Handler : IRequestHandler<UIUpdateProductActiveCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIUpdateProductActiveCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, UpdateProductActiveVM>(request.ProductStatusUpdate, $"Products/UpdateProductStatus");

            return res.IsSuccess() ?
               new()
               {
                   Status = true,
                   StatusMessage = res.GetMessage(),
                   Data = res.Data
               }
               :
               new()
               {
                   Status = false,
                   StatusMessage = res.GetMessage()
               };
        }
    }
}
