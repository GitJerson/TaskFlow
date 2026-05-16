using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
using Middleware;

namespace Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]
    public class ApiKeysController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeysController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }


        [HttpGet]
        public async Task<IActionResult> GetApiKeys()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();  

            var keys = await _apiKeyService.GetApiKeysForUser(Guid.Parse(userId));
            return Ok(keys);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApiKey([FromBody] CreateKeyDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var apiKey = await _apiKeyService.CreateApiKey(Guid.Parse(userId), request);
            return Ok(apiKey);
        }

        [HttpPut("{keyId}")]
        public async Task<IActionResult> UpdateApiKey([FromRoute] Guid keyId, [FromBody] UpdateKeyDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var result = await _apiKeyService.UpdateApiKey(Guid.Parse(userId), keyId, request);
            return Ok(result);
        }

        [HttpDelete("{keyId}")]
        public async Task<IActionResult> RevokeApiKey([FromRoute] Guid keyId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var result = await _apiKeyService.RevokeApiKey(Guid.Parse(userId), keyId);
            return Ok(result);
        }
    }

}