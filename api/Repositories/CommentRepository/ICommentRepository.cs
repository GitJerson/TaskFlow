namespace Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task<Comment?> FindCommentById(Guid commentId);
        Task<ICollection<Comment>> FindCommentsByTasksId(Guid taskId);
        Task UpdateComment(Comment comment);
    }
}