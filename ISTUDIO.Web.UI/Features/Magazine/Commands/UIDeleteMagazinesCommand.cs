namespace ISTUDIO.Web.UI.Features.Magazine.Commands;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteMagazinesCommand : IRequest<ResponseAPI<ResModel>>
{

    [Required]
    public int MagazineId { get; set; }
    public class Handler : IRequestHandler<UIDeleteMagazinesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteMagazinesCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Magazines/DeleteMagazine?magazineId={command.MagazineId}");
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
