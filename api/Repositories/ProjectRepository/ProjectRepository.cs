using Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        //Context
        private readonly AppDbContext _dbContext;

        //Constructor & Dependency Injection
        public ProjectRepository(AppDbContext dbContext)
        {
           _dbContext = dbContext; 
        }
        
        //Methods
        public async Task<Project?> FindProject(Guid id)
        {
            var project = await _dbContext.Projects
                        .Include(u => u.User)
                        .FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }
        public async Task AddProject(Project project)
        {
            await _dbContext.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProject(Project project)
        {
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProject(Project project)
        {
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Project>> GetProjects()
        {
            var projects = await _dbContext.Projects
                                .Include(p => p.User)
                                .ToListAsync();
            return projects;
        }
    }
}