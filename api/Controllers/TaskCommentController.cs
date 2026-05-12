using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Controllers
{
    [ApiController]
    [Route("api/Tasks/{id}/comments")]
    [Authorize]
    public class TaskCommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public TaskCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByTaskId([FromRoute] Guid id)
        {
            var comments = await _commentService.FindCommentsByTaskIdAsync(id);

            if (comments == null)
                return NotFound();

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromRoute] Guid id, [FromBody] CreateCommentDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var message = await _commentService.AddCommentAsync(Guid.Parse(userId), id, request);

            return Ok(message);
        }

    }
}