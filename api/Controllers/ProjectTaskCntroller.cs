using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Dtos;
namespace Controllers
{
    [ApiController]
    [Route("api/Project/{projectId}/tasks")]
    [Authorize]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ITaskService  _taskService;
        
        public ProjectTaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromRoute] Guid projectId, [FromBody] CreateTaskDto request)
        {
            var message = await _taskService.AddTaskAsync(projectId, request);

            return Ok(message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks([FromRoute] Guid projectId)
        {
            var tasks = await _taskService.FindTasksByProjectIdAsync(projectId);

            if(tasks == null)
                return NotFound();

            return Ok(tasks);
        }


    }
}