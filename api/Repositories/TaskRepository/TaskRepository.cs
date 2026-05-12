using Data;
using Dtos;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        //Implementations
        public async Task AddTask(ProjectTask task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateTask(ProjectTask task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProjectTask?> FindTaskById(Guid taskId)
        {
            var task = await _dbContext.Tasks
                        .Include(t => t.AssignedUser)
                        .FirstOrDefaultAsync(t => t.Id == taskId);

            return task;
        }

        public async Task<ICollection<ProjectTask>> FindTasksByProjectId(Guid projectId)
        {
            var tasks = await _dbContext.Tasks
                                .Include(t => t.AssignedUser)
                                .Where(t => t.ProjectId == projectId)
                                .ToListAsync();
            return tasks;
        }


    }
}