using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _dbContext;
        public CommentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddComment(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Comment?> FindCommentById(Guid commentId)
        {
            var comment = await _dbContext.Comments
                        .Include(c => c.User)
                        .FirstOrDefaultAsync(c => c.Id == commentId);

            return comment;

        }

        public async Task<ICollection<Comment>> FindCommentsByTasksId(Guid taskId)
        {
            var comments = await _dbContext.Comments
                        .Include(c => c.User)
                        .Where(c => c.ProjectTaskId == taskId && c.IsActive)
                        .ToListAsync();

            return comments;
        }

        public async Task UpdateComment(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
} 
