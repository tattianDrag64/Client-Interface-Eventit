
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Client_Interface_Eventit.Models.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace Client_Interface_Eventit.Auth
{

    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly TokenStorage _tokenStorage;

        public ApiAuthenticationStateProvider(TokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokens = await _tokenStorage.GetToken();

            if (tokens == null || string.IsNullOrWhiteSpace(tokens.AccessToken))
            {
                // Not logged in
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = BuildClaimsIdentity(tokens.AccessToken);
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void NotifyUserAuthentication(string accessToken)
        {
            var identity = BuildClaimsIdentity(accessToken);
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        private ClaimsIdentity BuildClaimsIdentity(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);

            return new ClaimsIdentity(jwt.Claims, "jwt");
        }
    }

}
