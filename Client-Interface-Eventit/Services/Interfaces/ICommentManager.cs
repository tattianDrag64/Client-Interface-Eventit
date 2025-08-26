using System.Net.Http.Json;
//using static Client_Interface_Eventit.Pages.CommentPages.CommentAdd;
//using static Client_Interface_Eventit.Pages.CommentPages.CommentEdit;
//using static Client_Interface_Eventit.Pages.CommentPages.Comments;
//using static Client_Interface_Eventit.Pages.CommentPages.EventComments;
using Client_Interface_Eventit.ApiClient;

namespace Services.Interfaces
{
    public interface ICommentManager
    {
        Task<List<CommentDTO>> GetAllComments();
        Task<CommentDTO> GetByIdComment(int id);
        Task<bool> CreateComment(CreateCommentDto comment);
        Task UpdateComment(UpdateCommentDto comment);
        Task<bool> DeleteComment(int id);
        Task<List<CommentDTO>> GetByUserIdComment(Guid userId);
        Task<List<CommentDTO>> GetByEventIdComment(int eventId);
    }
}
