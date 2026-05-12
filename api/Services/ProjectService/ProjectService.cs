using Dtos;
using DTOs;
using Models;
using Repositories;

namespace Services
{
    public class ProjectService : IProjectService
    {
        //Repository
        private readonly IProjectRepository _projectRepository;

        //Constructor & Dependency injection
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        //Methods
        public async Task<string> CreateProjectAsync(Guid userId, CreateProjectDto request)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = true,
                UserId = userId
            };

            await _projectRepository.AddProject(project);

            return "Project created successfully";
        }

        public async Task<string> DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository.FindProject(id);

            if(project == null)
                return "Project not found";
            
            project.IsActive = false;

            await _projectRepository.UpdateProject(project);

            return "Project deleted successfully";
        }

        public async Task<ProjectResponseDto> GetProjectAsync(Guid id)
        {
            var project = await _projectRepository.FindProject(id);

            if(project == null)
                return null!;

            ProjectResponseDto projects = new ProjectResponseDto()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                IsActive = project.IsActive,
                OwnerName = project?.User?.Name
            };
            
            return projects;
        }

        public async Task<ICollection<ProjectResponseDto>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetProjects();

            var projectDtos = projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                IsActive = p.IsActive,
                OwnerName = p?.User?.Name
            }).ToList();

            return projectDtos;
        }

        public async Task<string> UpdateProjectAsync(Guid id, UpdateProjectDto request)
        {
            var project = await _projectRepository.FindProject(id);

            if(project == null)
                return "Project not found";
            
            project.Title = request.Title;
            project.Description = request.Description;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.UpdateProject(project);

            return "Project updated successfully";
        }
    }
}