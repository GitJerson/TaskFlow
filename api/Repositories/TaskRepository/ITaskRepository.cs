using Dtos;
using Models;

namespace Repositories
{
    public interface ITaskRepository
    {
        Task<ICollection<ProjectTask>> FindTasksByProjectId(Guid projectId);
        Task<ProjectTask?> FindTaskById(Guid taskId);
        Task AddTask(ProjectTask task);
        Task UpdateTask(ProjectTask task);
    }
}