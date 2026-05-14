using System.Text.Json;
using Dtos;
using DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Models;
using Repositories;

namespace Services
{
    public class ProjectService : IProjectService
    {
        //Repository
        private readonly IProjectRepository _projectRepository;
        private readonly IDistributedCache _cache;

        //Constructor & Dependency injection
        public ProjectService(IProjectRepository projectRepository, IDistributedCache cache)
        {
            _projectRepository = projectRepository;
            _cache = cache;
        }


        //Methods
        public async Task<string> CreateProjectAsync(Guid userId, CreateProjectDto request)
        {
            var project = new Project()
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

            await _cache.RemoveAsync("projects");
            Console.WriteLine("Cache removed: projects");

            return "Project created successfully";
        }

        public async Task<string> DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository.FindProject(id);

            if(project == null)
                return "Project not found";
            
            project.IsActive = false;

            await _projectRepository.UpdateProject(project);

            await _cache.RemoveAsync($"project:{id}");
            await _cache.RemoveAsync("projects");

            return "Project deleted successfully";
        }

        public async Task<ProjectResponseDto> GetProjectAsync(Guid id)
        {
            var cachedProject = await _cache.GetStringAsync($"project:{id}");
            if (cachedProject != null)
            {
                return JsonSerializer.Deserialize<ProjectResponseDto>(cachedProject)!;
            }

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
            
            // Cache the result
            var serializedProject = JsonSerializer.Serialize(projects);
            await _cache.SetStringAsync($"project:{id}", serializedProject, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return projects;
        }

        public async Task<ICollection<ProjectResponseDto>> GetProjectsAsync()
        {
            var cachedProjects = await _cache.GetStringAsync("projects");
            if (cachedProjects != null)
            {
                return JsonSerializer.Deserialize<ICollection<ProjectResponseDto>>(cachedProjects)!;
            }

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


            // Cache the result
            var serializedProjects = JsonSerializer.Serialize(projectDtos);
            await _cache.SetStringAsync("projects", serializedProjects, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
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

            await _cache.RemoveAsync("projects");
            await _cache.RemoveAsync($"project:{id}");

            return "Project updated successfully";
        }
    }
}