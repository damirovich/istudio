namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIUpdateProductSummaCommand : IRequest<ResponseAPI<ResModel>>
{
    public UpdateProductSummVM ProductSummaUpdate { get; set; }

    public class Handler : IRequestHandler<UIUpdateProductSummaCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIUpdateProductSummaCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, UpdateProductSummVM>(request.ProductSummaUpdate, $"Products/UpdateProductSumm");

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
