using ISTUDIO.Application.Features.ShoppingCarts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.ShoppingCarts.Queries;
using ResModel = PaginatedList<ActualShopCartsResponseDTO>;
public class UIGetShoppingCartsQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
    public class Handler : IRequestHandler<UIGetShoppingCartsQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"ShopingCarts/GetShoppingCarts?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
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
