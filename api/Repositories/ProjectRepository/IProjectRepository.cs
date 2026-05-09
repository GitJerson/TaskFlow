using Models;
namespace Repositories
{
    public interface IProjectRepository
    {
        //Contracts
        Task AddProject(Project project);
        Task UpdateProject(Project project);
        Task DeleteProject(Project project);
        Task<ICollection<Project>> GetProjects();
        Task<Project?> FindProject(Guid id);

    }
}