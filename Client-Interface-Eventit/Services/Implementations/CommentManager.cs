using Client_Interface_Eventit.Pages.CommentPages;
using Services.Interfaces;
using System.Net.Http.Json;
//using static Client_Interface_Eventit.Pages.CommentPages.Comments;
using Client_Interface_Eventit.ApiClient;
using System.Net.Http;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Services.Implementations
{
    public class CommentManager(HttpClient http, IClient client, ILocalStorageService localStorage) : ICommentManager
    {
        private readonly HttpClient _http = http ?? throw new ArgumentNullException(nameof(http));
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public async Task<bool> CreateComment(CreateCommentDto comment)
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("Token");
                _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(token);
                var response = await _http.PostAsJsonAsync("api/сomments", comment);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating comment: {ex.Message}");
                return false;
            }
        }

        public Task<bool> DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentDTO>> GetAllComments()
        {
            var comments = await _http.GetFromJsonAsync<List<CommentDTO>>("/api/comments");
            if (comments == null)
                return null;
            return comments;
        }

        public Task<List<CommentDTO>> GetByEventIdComment(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentDTO> GetByIdComment(int id)
        {
            var comment = await _http.GetFromJsonAsync<CommentDTO>($"/api/comments/{id}");
            if (comment == null)
                return null;
            return comment;
        }

        public Task<List<CommentDTO>> GetByUserIdComment(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateComment(UpdateCommentDto comment)
        {
            await _http.PutAsJsonAsync($"api/comments/{comment.Id}", comment);
        }

        
    }
}