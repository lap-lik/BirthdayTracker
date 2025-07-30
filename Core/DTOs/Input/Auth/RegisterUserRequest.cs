using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Input.Auth
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Email,
        [Required] string Password,
        IFormFile? Avatar);
}