using Dtos;
using Models;
using Repositories;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<string> AddTaskAsync(Guid projectId, CreateTaskDto request)
        {
            ProjectTask task = new ProjectTask()
            {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = true,
                IsCompleted = false,
                DueDate = request.DueDate,
                Priority = request.Priority,
                AssignedUserId = request.AssignedUserId
            };

            await _taskRepository.AddTask(task);

            return "Task created successfully";
        }

        public async Task<string> DeleteTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.FindTaskById(taskId);

            if(task == null)
                return "Task not found";
            
            task.IsActive = false;

            await _taskRepository.UpdateTask(task);

            return "Task deleted successfully";
        }

        public async Task<TaskResponseDto?> FindTaskByIdAsync(Guid taskId)
        {
            var task = await _taskRepository.FindTaskById(taskId);

            if(task == null)
                return null!;

            TaskResponseDto tasks = new TaskResponseDto()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                IsActive = task.IsActive,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                Priority = task.Priority,
                AssignedTo = task.AssignedUser?.Name
            };
            
            return tasks;
        }

        public async Task<ICollection<TaskResponseDto>> FindTasksByProjectIdAsync(Guid projectId)
        {
            var tasks = await _taskRepository.FindTasksByProjectId(projectId);

            var taskDtos = tasks.Select(task => new TaskResponseDto()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                IsActive = task.IsActive,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                Priority = task.Priority,
                AssignedTo = task.AssignedUser?.Name
            }).ToList();

            return taskDtos;
        }

        public async Task<string> UpdateTaskAsync(Guid taskId, UpdateTaskDto request)
        {
            var task = await _taskRepository.FindTaskById(taskId);

            if(task == null)
                return "Task not found";

            task.Title = request.Title;
            task.Description = request.Description;
            task.UpdatedAt = DateTime.UtcNow;
            task.DueDate = request.DueDate;
            task.Priority = request.Priority;
            task.AssignedUserId = request.AssignedUserId;
            task.IsCompleted = request.IsCompleted;
            await _taskRepository.UpdateTask(task);

            return "Task updated successfully";
        }
    }
}