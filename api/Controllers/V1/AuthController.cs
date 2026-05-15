namespace Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using DTOs;
    using Services;
    using Microsoft.AspNetCore.Authorization;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Google;
    using System.Security.Claims;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //Calls login service
            var user = await _authService.LoginAsync(request);

            if(user == null)
                return Unauthorized();

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Calls register service
            var message = await _authService.RegisterAsync(request);

            if(message == "Email already exists")
                return Conflict();

            return Ok(message);
        }

        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
           var redirectUrl = Url.Action("GoogleCallback", "Auth");
           var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
           
           return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest();

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

            var token = await _authService.GoogleLoginAsync(email!, name!);

            if(token == null)
                return Unauthorized();

            return Ok(token);
        }
    }

}