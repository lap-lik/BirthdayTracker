using API.Controllers;
using Core.DTOs.Input.User;
using Core.DTOs.Output.User;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayTracker.Controllers
{
    [Route("user")]
    public class UserController : AuthorizedController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPatch("profile")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<UserResponse>> UpdateProfile([FromForm] UpdateUserRequest request)
        {
            var result = await _userService.UpdateAsync(AuthorizedUserId, request);
            return Ok(result);
        }

        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _userService.UpdatePasswordAsync(AuthorizedUserId, request);
            return NoContent();
        }
    }
}