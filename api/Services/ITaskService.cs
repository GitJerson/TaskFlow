using Models;
using Dtos;
namespace Services
{
    public interface ITaskService
    {
        Task<string> AddTaskAsync(Guid projectId, CreateTaskDto request);
        Task<TaskResponseDto?> FindTaskByIdAsync(Guid taskId);
        Task<ICollection<TaskResponseDto>> FindTasksByProjectIdAsync(Guid projectId);
        Task<string> UpdateTaskAsync(Guid taskId, UpdateTaskDto request);
        Task<string> DeleteTaskAsync(Guid taskId);
    }
}