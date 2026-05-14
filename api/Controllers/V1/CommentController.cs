using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
using Asp.Versioning;
namespace Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            var comment = await _commentService.FindCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] UpdateCommentDto request)
        {
            var message = await _commentService.UpdateCommentAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var message = await _commentService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}