using DTOs;

namespace Services
{
    public interface ICommentService
    {
        Task<string> AddCommentAsync(Guid userId, Guid taskId, CreateCommentDto request);
        Task<CommentResponseDto?> FindCommentByIdAsync(Guid commentId);
        Task<ICollection<CommentResponseDto>> FindCommentsByTaskIdAsync(Guid taskId);
        Task<string> UpdateCommentAsync(Guid commentId, UpdateCommentDto request);
        Task<string> DeleteCommentAsync(Guid commentId);
    }
}