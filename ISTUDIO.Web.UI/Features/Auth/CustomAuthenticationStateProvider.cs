using ISTUDIO.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace ISTUDIO.Web.UI.Features.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly NavigationManager _navigationManager;

    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, NavigationManager navigationManager)
    {
        _sessionStorage = sessionStorage;
        _navigationManager = navigationManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSessions>("UserSession");
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

            if (userSession is null)
            {
                await Logout();
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim("access_jwtToken", userSession.AccessToken),
                new Claim("refresh_jwtToken", userSession.RefreshToken)
            };

            foreach (var userRole in userSession.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authClaims, "CustomAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSessions? userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _sessionStorage.SetAsync("UserSession", userSession);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim("access_jwtToken", userSession.AccessToken),
                new Claim("refresh_jwtToken", userSession.RefreshToken)

            };

            foreach (var userRole in userSession.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authClaims, "CustomAuth"));
        }
        else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    //Выход
    public async Task Logout()
    {
        _navigationManager.NavigateTo("/login");
    }
}
