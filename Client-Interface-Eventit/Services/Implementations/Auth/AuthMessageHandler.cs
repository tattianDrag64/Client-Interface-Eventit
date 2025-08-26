using Client_Interface_Eventit.Auth;
using System.Net.Http.Headers;

namespace Client_Interface_Eventit.Services.Implementations.Auth
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly TokenStorage _tokenStorage;

        public AuthMessageHandler(TokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get tokens from localStorage
            var tokens = await _tokenStorage.GetToken();

            if (tokens != null && !string.IsNullOrWhiteSpace(tokens.AccessToken))
            {
                // Attach access token
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            }

            // Continue sending request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

