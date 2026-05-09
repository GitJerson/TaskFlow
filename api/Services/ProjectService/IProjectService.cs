namespace Services
{
    using Dtos;
    using DTOs;
    using Models;

    public interface IProjectService
    {
        Task<string> CreateProjectAsync(Guid userId, CreateProjectDto request);
        Task<ProjectResponseDto> GetProjectAsync(Guid id);
        Task<ICollection<ProjectResponseDto>> GetProjectsAsync();
        Task<string>  UpdateProjectAsync(Guid id, UpdateProjectDto request);
        Task<string> DeleteProjectAsync(Guid id);
    }
}