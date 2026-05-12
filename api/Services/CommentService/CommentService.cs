using DTOs;
using Repositories;
using Models;
namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<string> AddCommentAsync(Guid userId, Guid taskId, CreateCommentDto request)
        {
            var newComment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = true,
                UserId = userId,
                ProjectTaskId = taskId
            };

            await _commentRepository.AddComment(newComment);
            return "Comment added successfully.";
        }


        public async Task<CommentResponseDto?> FindCommentByIdAsync(Guid commentId)
        {
            var comments = await _commentRepository.FindCommentById(commentId);

            if(comments == null)
                return null!;
            
            CommentResponseDto comment = new CommentResponseDto
            {
                Id = comments.Id,
                Content = comments.Content,
                CreatedAt = comments.CreatedAt,
                UpdatedAt = comments.UpdatedAt,
                IsActive = comments.IsActive,
                UserId = comments.UserId,
                UserName = comments.User?.Name,
                ProjectTaskId = comments.ProjectTaskId
            };

            return comment;
        }
        public async Task<ICollection<CommentResponseDto>> FindCommentsByTaskIdAsync(Guid taskId)
        {
            var comments = await _commentRepository.FindCommentsByTasksId(taskId);
            var commentDtos = comments.Select(c => new CommentResponseDto
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                UserName = c.User?.Name,
                ProjectTaskId = c.ProjectTaskId
            }).ToList();

            return commentDtos;
        }

        public async Task<string> UpdateCommentAsync(Guid commentId, UpdateCommentDto request)
        {
            var comment = await _commentRepository.FindCommentById(commentId);

            if(comment == null)
                return "Comment not found.";

            comment.Content = request.Content;
            comment.IsActive = request.IsActive;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.UpdateComment(comment);
                return "Comment updated successfully.";
        }
        public async Task<string> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.FindCommentById(commentId);

            if(comment == null)
                return "Comment not found.";

            comment.IsActive = false;
            await _commentRepository.UpdateComment(comment);
            return "Comment deleted successfully.";
        }
    }
}