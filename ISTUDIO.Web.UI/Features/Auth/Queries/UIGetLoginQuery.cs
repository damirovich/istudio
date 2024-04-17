using ISTUDIO.Application.Features.Authentication.DTOs;
namespace ISTUDIO.Web.UI.Features.Auth.Queries;

using ISTUDIO.Contracts.Features.Authentication.Authorizations;
using Microsoft.AspNetCore.Components.Authorization;
using ResModel = AuthResponseDTO;
public class UIGetLoginQuery : IRequest<ResponseAPI<ResModel>>
{
    public LoginVM  Login { get; set; }
    public class Handler : IRequestHandler<UIGetLoginQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        public Handler(APIHttpClient apiClient, AuthenticationStateProvider authStateProvider)
        {
            _apiClient = apiClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetLoginQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, LoginVM>(request.Login, $"Auth/Login/Login");
            if (res.IsSuccess())
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(res.Data.UserSession);
            }
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