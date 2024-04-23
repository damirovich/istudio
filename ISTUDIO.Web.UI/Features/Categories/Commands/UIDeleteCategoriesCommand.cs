namespace ISTUDIO.Web.UI.Features.Categories.Commands;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteCategoriesCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int CategoryId { get; set; }

    public class Handler : IRequestHandler<UIDeleteCategoriesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteCategoriesCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Categories/DeleteCategory?Id={command.CategoryId}");
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
