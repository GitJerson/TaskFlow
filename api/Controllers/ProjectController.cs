namespace Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using DTOs;
    using Services;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;
    using Dtos;
    using Models;
    using Microsoft.AspNetCore.Identity.UI.Services;

    [Route("api/Project")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();

            if(projects == null || !projects.Any())
                return NotFound();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute] Guid id)
        {

            var project = await _projectService.GetProjectAsync(id);

            if(project == null)
                return NotFound();
            
            return Ok(project);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
                return Unauthorized();

            var project = await _projectService.CreateProjectAsync(Guid.Parse(userId), request);

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] UpdateProjectDto request)
        {
            var project = await _projectService.UpdateProjectAsync(id, request);

            if(project == "Project not found")
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid id)
        {
            var project = await _projectService.DeleteProjectAsync(id);
            if(project == "Project not found")
                return NotFound();
            return NoContent();
        }

    }
}