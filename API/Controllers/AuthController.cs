using BirthdayTracker.Controllers;
using Core.DTOs.Input.Auth;
using Core.DTOs.Output.Auth;
using Core.Intefaces.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    public class AuthController : BaseController
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Register([FromForm] RegisterUserRequest request)
        {
            await _authService.Register(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserRequest request)
        {
            var result = await _authService.Login(request);
            return Ok(result);
        }
    }
}