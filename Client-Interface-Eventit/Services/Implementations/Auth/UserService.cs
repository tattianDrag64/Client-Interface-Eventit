using Client_Interface_Eventit.Models.Auth;
using System.Net.Http.Json;
using static Client_Interface_Eventit.Pages.Auth.UsersTest;
using Client_Interface_Eventit.Auth;

namespace Services.Implementations.Auth
{//test comment
    public class UserService
    {
        private readonly HttpClient _http;
        private readonly TokenStorage _tokenStorage;

        public UserService(HttpClient http, TokenStorage tokenStorage)
        {
            _http = http;
            _tokenStorage = tokenStorage;
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await _http.GetFromJsonAsync<List<UserDTO>>("users") ?? new List<UserDTO>();
        }

        public async Task<bool> Register(CreateUserDto dto)
        {
            var response = await _http.PostAsJsonAsync("users", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(LoginUserDto dto)
        {
            var response = await _http.PostAsJsonAsync("users/login", dto);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            if (result == null || string.IsNullOrEmpty(result.AccessToken))
                return false;

            await _tokenStorage.SetToken(result);
            return true;
        }

        public async Task Logout() => await _tokenStorage.ClearToken();

        public async Task<TokenResponseDto?> GetTokens()
        {
            return await _tokenStorage.GetToken();
        }

    }
}

