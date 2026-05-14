using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Dtos;
using Asp.Versioning;
namespace Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/tasks/{taskId}")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid taskId)
        {
            var task = await _taskService.FindTaskByIdAsync(taskId);

            if(task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid taskId, [FromBody] UpdateTaskDto request)
        {
            var task = await _taskService.UpdateTaskAsync(taskId, request);

            if(task == "Task not found")
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            var task = await _taskService.DeleteTaskAsync(taskId);

            if(task == "Task not found")
                return NotFound();

            return NoContent();
        }

    }
}