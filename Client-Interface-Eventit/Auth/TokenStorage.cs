using Microsoft.JSInterop;
using Client_Interface_Eventit.Models.Auth;
using System.Text.Json;

namespace Client_Interface_Eventit.Auth
{
    public class TokenStorage
    {
        private readonly IJSRuntime _js;
        private const string TokenKey = "authTokens";

        public TokenStorage(IJSRuntime js)
        {
            _js = js;
        }

        // Save tokens
        public async Task SetToken(TokenResponseDto tokenResponse)
        {
            var json = JsonSerializer.Serialize(tokenResponse);
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, json);
        }

        // Get tokens
        public async Task<TokenResponseDto?> GetToken()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonSerializer.Deserialize<TokenResponseDto>(json);
        }

        // Remove tokens (logout)
        public async Task ClearToken()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
    }
}

